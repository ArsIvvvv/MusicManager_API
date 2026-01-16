using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MusicMicroservice.Infrastructure.Data;

namespace MusicMicroservice.Music.API.Extensions
{
    public static class ApplicationInitializationExtensions
    {
        public async static void InitializeApplication(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            try
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await context.Database.MigrateAsync();
                app.Logger.LogInformation("------------Migrate success!----------------");

                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                string[] roleNames = { "DefaultUser", "Admin" };

                foreach (var role in roleNames)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                app.Logger.LogInformation("------------Roles seeded successfully!----------------");
            }
            catch (Exception ex)
            {
                app.Logger.LogError(ex, "------------Migration or role seeding failed!----------------");
                throw; 
            }
        }
    }
}