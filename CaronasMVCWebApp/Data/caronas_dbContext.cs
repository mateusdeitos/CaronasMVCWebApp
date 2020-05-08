using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using CaronasMVCWebApp.Models;

namespace CaronasMVCWebApp
{
    public partial class caronas_dbContext : DbContext
    {
        public caronas_dbContext()
        {
        }

        public caronas_dbContext(DbContextOptions<caronas_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Destiny> Destiny { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<Ride> Ride { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();

                optionsBuilder.UseSqlServer(configuration.GetConnectionString("caronas_app_dbContext"));
                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Destiny>(entity =>
            {
                entity.ToTable("destiny", "caronas_app_db");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60);
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("member", "caronas_app_db");

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60);
            });

            modelBuilder.Entity<Ride>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.DestinyId, e.DriverId, e.PassengerId });

                entity.ToTable("ride", "caronas_app_db");

                entity.HasIndex(e => e.DestinyId)
                    .HasName("IX_Ride_DestinyId");

                entity.HasIndex(e => e.DriverId)
                    .HasName("IX_Ride_DriverId");

                entity.HasIndex(e => e.PassengerId)
                    .HasName("FK_Ride_Member_PassengerId_idx");

                entity.Property(e => e.Date).HasColumnType("datetime2(6)");

                entity.Property(e => e.PaymentStatus).HasColumnName("paymentStatus");

                entity.Property(e => e.RoundTrip).HasColumnName("roundTrip");

                entity.HasOne(d => d.Destiny)
                    .WithMany(p => p.Ride)
                    .HasForeignKey(d => d.DestinyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ride$FK_Ride_Destiny_DestinyId");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.RideDriver)
                    .HasForeignKey(d => d.DriverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ride$FK_Ride_Member_DriverId");

                entity.HasOne(d => d.Passenger)
                    .WithMany(p => p.RidePassenger)
                    .HasForeignKey(d => d.PassengerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ride$FK_Ride_Member_PassengerId");
            });
        }
    }
}
