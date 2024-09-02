using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BookingWebService.Models;

public partial class BonvoyageBookingDbContext : DbContext
{
    public BonvoyageBookingDbContext()
    {
    }

    public BonvoyageBookingDbContext(DbContextOptions<BonvoyageBookingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database=BonvoyageBookingDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Booking__C6D03BEDC83B9A19");

            entity.ToTable("Booking");

            entity.Property(e => e.BookingId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("bookingID");
            entity.Property(e => e.BookingPerson).HasColumnName("bookingPerson");
            entity.Property(e => e.BookingRooms).HasColumnName("bookingRooms");
            entity.Property(e => e.Bookingdate).HasColumnName("bookingdate");
            entity.Property(e => e.PackageId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("packageID");
            entity.Property(e => e.PackageImage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("packageImage");
            entity.Property(e => e.PackageName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("packageName");
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("userID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
