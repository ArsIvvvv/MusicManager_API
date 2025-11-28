using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusicMicroservice.Application.Common.Interfaces.Persistance;
using MusicMicroservice.Infrastructure.Data;
using MusicMicroservice.Infrastructure.Data.Repositories;

namespace MusicMicroservice.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddEfCoreRepositories(services);

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
       services.AddScoped<IExecuterRepository, ExecuterRepository>();
       services.AddScoped<IMusicRepository, MusicRepository>();
    }
}