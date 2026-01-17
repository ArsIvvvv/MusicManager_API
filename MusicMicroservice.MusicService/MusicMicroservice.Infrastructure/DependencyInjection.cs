using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusicMicroservice.Application.Common.Interfaces.Persistance;
using MusicMicroservice.Application.Common.Interfaces.Persistance.Kafka;
using MusicMicroservice.Application.Common.Interfaces.Persistance.Redis;
using MusicMicroservice.Contracts.Responses.Music;
using MusicMicroservice.Domain.Entities;
using MusicMicroservice.Domain.Entities.Identity;
using MusicMicroservice.Infrastructure.Data;
using MusicMicroservice.Infrastructure.Data.Cache;
using MusicMicroservice.Infrastructure.Data.Repositories;
using MusicMicroservice.Infrastructure.Kafka;

namespace MusicMicroservice.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);

        AddIdentity(services);

        AddEfCoreRepositories(services);
        
        AddRedisCaching(services, configuration);

        AddKafkaProducer<MusicResponse>(services);

        return services;
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionStringDb = configuration.GetConnectionString("DefaultConnection");

        // Регистрация контекста базы данных с использованием Npgsql
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionStringDb);
        });
    }

    private static void AddEfCoreRepositories(IServiceCollection services)
    {
       services.AddScoped<IExecutorRepository, ExecutorRepository>();
       services.AddScoped<IMusicRepository, MusicRepository>();
    }

    private static void AddRedisCaching(IServiceCollection services, IConfiguration configuration)
    {
        // Получение настроек Redis из конфигурации
        var redisConnectionString = configuration["Redis:RedisConnectionString"];
        var instanceName = configuration["Redis:InstanceName"];

        // Регистрация кэша Redis
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisConnectionString;
            options.InstanceName = instanceName;
        });

        services.AddScoped<ICacheService, RedisDistributedCacheService>();
    }

    private static void AddIdentity(IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();
    
    }

    private static void AddKafkaProducer<TMessage>(IServiceCollection services)
    {
        services.AddSingleton<IKafkaProducer, KafkaProducer>();
    }
}