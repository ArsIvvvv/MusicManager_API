using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MusicMicroservice.Application.Authentication;
using MusicMicroservice.Application.Authorization.Handlers;
using MusicMicroservice.Application.Authorization.Requirements;
using MusicMicroservice.Application.Common.Behaviors;
using MusicMicroservice.Application.Common.Interfaces.Authentication;
using MusicMicroservice.Application.Common.Interfaces.HttpService;
using MusicMicroservice.Application.Common.Interfaces.Persistance.Identity;
using MusicMicroservice.Application.HttpService;
using MusicMicroservice.Application.Services.Identity;
using MusicMicroservice.Contracts.Responses.MusicRatingDetails;
using MusicMicroservice.Contracts.Responses.Rating;
using MusicMicroservice.Domain.Entities;
using Polly;
using Polly.Contrib.Simmy;
using Polly.Extensions.Http;
using Polly.Timeout;

namespace MusicMicroservice.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services,  IConfiguration configuration)
        {

            AddHttpClientFactory(services);
            AddMediatR(services);   

            AddAuthentication(services, configuration);
            AddAuthorization(services);

            return services;
        }

        private static void AddMediatR(IServiceCollection services)
        {
           services.AddMediatR(cfg => 
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }

        private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = false,

                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Secret"]!))
                };
            });

            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
        }
        private static void AddAuthorization(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("OlderThan18", policy => policy.Requirements.Add(new AgeRequirements(18)));
            });

            services.AddSingleton<IAuthorizationHandler, AgeHandler>();
        }

        private static void AddHttpClientFactory(IServiceCollection services)
        {
            var fallback = Policy<HttpResponseMessage>
                .Handle<Polly.CircuitBreaker.BrokenCircuitException>()
                .FallbackAsync(fallbackAction: async (ct) =>
                {
                    var fallbackReview = new MusicRatingDetailsResponse(new List<MusicRatingReviewResponse>(),"empty");

                    var fallbackResponse = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(JsonSerializer.Serialize(new List<MusicRatingDetailsResponse> { fallbackReview }), Encoding.UTF8, "application/json")
                    };

                    return await Task.FromResult(fallbackResponse);
                },
                onFallbackAsync: (ex) =>
                {
                    Console.WriteLine($"Fallback triggered: {ex.Exception.Message}");
                    return Task.CompletedTask;
                });

            var circuitBreakerPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));

            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(
                    retryCount: 3,
                    sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)));

            var timeoutPolicy = Policy
                .TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(3), TimeoutStrategy.Optimistic);

            var chaosPolicy = MonkeyPolicy.InjectFaultAsync<HttpResponseMessage>(
                injectionRate: 0.2,
                fault: new HttpRequestException("Chaooooosss! (20%)"),
                enabled: () => true
                );

            var policyWrap = Policy.WrapAsync(fallback, circuitBreakerPolicy, retryPolicy, timeoutPolicy, chaosPolicy);

             services.AddHttpClient<IMusicRatingHttpService, MusicRatingHttpService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5097"); 
            })
            .AddPolicyHandler(policyWrap);
        }
        
    }
}