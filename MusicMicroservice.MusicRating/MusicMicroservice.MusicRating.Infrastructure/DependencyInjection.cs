using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MusicMicroservice.MusicRating.Application.Common.Interfaces.Persistence;
using MusicMicroservice.MusicRating.Infrastructure.Repositories;
using MongoDB.Driver;

namespace MusicMicroservice.MusicRating.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddMongoDb(services, configuration);
            
            AddRepositories(services);

            return services;
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IMusicRatingRepository, MusicRatingRepository>();
        }

        private static void AddMongoDb(IServiceCollection services, IConfiguration configuration)
        {
            var mongoConnectionString = configuration["Mongo:MongoConnectionString"];
            var mongoDatabaseName = configuration["Mongo:DatabaseName"];

            services.AddSingleton<IMongoClient>(new MongoClient(mongoConnectionString));
        }
    }
}