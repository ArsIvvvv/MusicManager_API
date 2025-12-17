using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MusicMicroservice.Application.Authentication;
using MusicMicroservice.Application.Common.Behaviors;
using MusicMicroservice.Application.Common.Interfaces.Authentication;
using MusicMicroservice.Application.Common.Interfaces.Persistance.Identity;
using MusicMicroservice.Application.Services.Identity;

namespace MusicMicroservice.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            AddMediatR(services);

            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddScoped<IUserService, UserService>();

            return services;
        }

        private static void AddMediatR(IServiceCollection services)
        {
           services.AddMediatR(cfg => 
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
        
    }
}