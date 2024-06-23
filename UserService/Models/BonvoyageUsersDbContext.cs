using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UserService.Models;

public partial class BonvoyageUsersDbContext : DbContext
{
    public BonvoyageUsersDbContext()
    {
    }

    public BonvoyageUsersDbContext(DbContextOptions<BonvoyageUsersDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            //Read the connection string from appsettings.json
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json")
                 .Build();
            var connectionString = builder.GetConnectionString("DBConnectionString");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__CB9A1CDF178C9F2C");

            entity.HasIndex(e => e.UserName, "UQ__Users__66DCF95C8DD2FB74").IsUnique();

            entity.HasIndex(e => e.UserEmail, "UQ__Users__D54ADF55540008AA").IsUnique();

            entity.HasIndex(e => e.UserPhone, "UQ__Users__E19C969116561133").IsUnique();

            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("userID");
            entity.Property(e => e.UserAddress)
                .HasColumnType("text")
                .HasColumnName("userAddress");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("userEmail");
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("userName");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("userPassword");
            entity.Property(e => e.UserPhone).HasColumnName("userPhone");
            entity.Property(e => e.UserRole)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("userRole");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
