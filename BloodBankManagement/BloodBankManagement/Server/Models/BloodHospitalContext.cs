using System;
using System.Collections.Generic;
using BloodBankManagement.Shared;
using Microsoft.EntityFrameworkCore;

namespace BloodBankManagement.Server.Models;

public partial class BloodHospitalContext : DbContext
{
    public BloodHospitalContext()
    {
    }

    public BloodHospitalContext(DbContextOptions<BloodHospitalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Doner> Doners { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsetting.json").Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("SqlConnection"));
        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doner>(entity =>
        {
			entity.HasKey(e => e.Id).HasName("PK__Doners__3214EC070A5909FA");

			entity.Property(e => e.BloodType)
				.HasMaxLength(50)
				.IsUnicode(false)
				.HasColumnName("Blood_Type");
			entity.Property(e => e.Lastname)
				.HasMaxLength(100)
				.IsUnicode(false);
			entity.Property(e => e.Name)
				.HasMaxLength(100)
				.IsUnicode(false);
		});

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
