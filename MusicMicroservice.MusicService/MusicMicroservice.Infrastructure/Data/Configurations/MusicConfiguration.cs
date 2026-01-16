using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicMicroservice.Domain.Entities;

namespace MusicMicroservice.Infrastructure.Data.Configurations;

public class MusicConfiguration: IEntityTypeConfiguration<Music>
{
public void Configure(EntityTypeBuilder<Music> builder)
{
    builder.ToTable("Musics");

    builder.HasKey(b => b.Id);

    builder.Property(b => b.Name)
        .IsRequired()
        .HasMaxLength(200);

    builder.Property(b => b.Year)
        .IsRequired();

    builder.HasMany(b => b.Executors)
        .WithMany(a => a.Musics)
        .UsingEntity<Dictionary<string, object>>(
        "MusicExecutors",
        r => r.HasOne<Executor>().WithMany().HasForeignKey("ExecutorId").OnDelete(DeleteBehavior.Restrict),
        l => l.HasOne<Music>().WithMany().HasForeignKey("MusicId").OnDelete(DeleteBehavior.Restrict)
        );
}
}