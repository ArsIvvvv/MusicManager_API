using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace MusicMicroservice.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            AddMediatR(services);

            return services;
        }


        private static void AddMediatR(IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
        
    }
}