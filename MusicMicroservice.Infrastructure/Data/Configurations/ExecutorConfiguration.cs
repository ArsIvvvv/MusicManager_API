using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicMicroservice.Domain.Entities;

namespace MusicMicroservice.Infrastructure.Data.Configurations;

public class ExecutorConfiguration: IEntityTypeConfiguration<Executor>
{
    public void Configure(EntityTypeBuilder<Executor> builder)
{
    builder.ToTable("Executors");

    builder.HasKey(b => b.Id);

    builder.Property(b => b.FirstName)
        .IsRequired()
        .HasMaxLength(200);

    builder.Property(b => b.LastName)
        .IsRequired()
        .HasMaxLength(200);

    builder.Property(b => b.Nickname)
        .IsRequired()
        .HasMaxLength(200);    
}
}