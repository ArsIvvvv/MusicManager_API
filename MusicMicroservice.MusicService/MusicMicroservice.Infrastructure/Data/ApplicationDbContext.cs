using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicMicroservice.Domain;
using MusicMicroservice.Domain.Entities;
using MusicMicroservice.Domain.Entities.Identity;

namespace MusicMicroservice.Infrastructure.Data;

public class ApplicationDbContext: IdentityDbContext<User>
{
    public DbSet<Music> Musics {get; set;} = null!;
    public DbSet<Executor> Executors {get; set;} = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
}