using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PackageWebService.Models;

public partial class BonvoyagePackageDbContext : DbContext
{
    public BonvoyagePackageDbContext()
    {
    }

    public BonvoyagePackageDbContext(DbContextOptions<BonvoyagePackageDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Package> Packages { get; set; }

    public virtual DbSet<Wishlist> Wishlists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=BonvoyagePackageDB;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Package>(entity =>
        {
            entity.HasKey(e => e.PackageId).HasName("PK__Packages__AA8D20E8487F2FD7");

            entity.HasIndex(e => e.PackageName, "UQ__Packages__94D7360E0567775B").IsUnique();

            entity.Property(e => e.PackageId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("packageID");
            entity.Property(e => e.AvailableDate).HasColumnName("availableDate");
            entity.Property(e => e.MaxPeople).HasColumnName("maxPeople");
            entity.Property(e => e.MinAge)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("minAge");
            entity.Property(e => e.PackageCity)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("packageCity");
            entity.Property(e => e.PackageCountry)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("packageCountry");
            entity.Property(e => e.PackageDesc)
                .HasColumnType("text")
                .HasColumnName("packageDesc");
            entity.Property(e => e.PackageDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("packageDuration");
            entity.Property(e => e.PackageImage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("packageImage");
            entity.Property(e => e.PackageIternary).HasColumnName("packageIternary");
            entity.Property(e => e.PackageLanguage)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("packageLanguage");
            entity.Property(e => e.PackageName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("packageName");
            entity.Property(e => e.PackagePickup)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("packagePickup");
            entity.Property(e => e.PackagePrice)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("packagePrice");
            entity.Property(e => e.PackageRating)
                .HasColumnType("decimal(2, 1)")
                .HasColumnName("packageRating");
            entity.Property(e => e.PackageReviews).HasColumnName("packageReviews");
        });

        modelBuilder.Entity<Wishlist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Wishlist__3214EC0749254FB8");

            entity.ToTable("Wishlist");

            entity.Property(e => e.PackageId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
