using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
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

namespace MusicMicroservice.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services,  IConfiguration configuration)
        {
            AddMediatR(services);

            services.AddHttpClient<IMusicRatingHttpService, MusicRatingHttpService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5097"); 
                client.Timeout = TimeSpan.FromSeconds(10);
            });

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
        
    }
}