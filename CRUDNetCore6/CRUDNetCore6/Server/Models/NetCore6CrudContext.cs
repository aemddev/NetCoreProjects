using System;
using System.Collections.Generic;
using CRUDNetCore6.Shared;
using Microsoft.EntityFrameworkCore;

namespace CRUDNetCore6.Server.Models;

public partial class NetCore6CrudContext : DbContext
{
    public NetCore6CrudContext()
    {
    }

    public NetCore6CrudContext(DbContextOptions<NetCore6CrudContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsetting.json")
            .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("SQL"));
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");

            entity.Property(e => e.Email)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Lastname)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.LeavingDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
