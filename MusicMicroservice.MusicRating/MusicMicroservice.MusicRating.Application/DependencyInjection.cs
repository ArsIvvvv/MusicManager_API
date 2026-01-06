using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MusicMicroservice.MusicRating.Application.Common.Interfaces.Service;
using MusicMicroservice.MusicRating.Application.Service;

namespace MusicMicroservice.MusicRating.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            AddApplicationServices(services);

            return services;
        }

        private static void AddApplicationServices(IServiceCollection services)
        {
            services.AddScoped<IMusicRatingService,MusicRatingService>();
        }
    }
}