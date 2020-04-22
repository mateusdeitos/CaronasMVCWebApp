using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CaronasMVCWebApp.Models
{
    public partial class caronas_app_dbContext : DbContext
    {
        public caronas_app_dbContext()
        {
        }

        public caronas_app_dbContext(DbContextOptions<caronas_app_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Destiny> Destiny { get; set; }
        public virtual DbSet<Efmigrationshistory> Efmigrationshistory { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<Passenger> Passenger { get; set; }
        public virtual DbSet<Ride> Ride { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;port=3306;user=developer;password=123456;database=caronas_app_db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Destiny>(entity =>
            {
                entity.ToTable("destiny");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(60)");
            });

            modelBuilder.Entity<Efmigrationshistory>(entity =>
            {
                entity.HasKey(e => e.MigrationId);

                entity.ToTable("__efmigrationshistory");

                entity.Property(e => e.MigrationId).HasColumnType("varchar(95)");

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasColumnType("varchar(32)");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("member");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("longtext");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(60)");

                entity.Property(e => e.Phone).HasColumnType("longtext");
            });

            modelBuilder.Entity<Passenger>(entity =>
            {
                entity.HasKey(e => new { e.PassengerId, e.RideId });

                entity.ToTable("passenger");

                entity.HasIndex(e => e.RideId)
                    .HasName("ride_id_idx");

                entity.Property(e => e.PassengerId).HasColumnName("passengerId");

                entity.Property(e => e.RideId).HasColumnName("rideId");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Passenger)
                    .HasForeignKey(d => d.PassengerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("member_id");

                entity.HasOne(d => d.Ride)
                    .WithMany(p => p.Passenger)
                    .HasForeignKey(d => d.RideId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ride_id");
            });

            modelBuilder.Entity<Ride>(entity =>
            {
                entity.ToTable("ride");

                entity.HasIndex(e => e.DestinyId)
                    .HasName("IX_Ride_DestinyId");

                entity.HasIndex(e => e.DriverId)
                    .HasName("IX_Ride_DriverId");

                entity.HasOne(d => d.Destiny)
                    .WithMany(p => p.Ride)
                    .HasForeignKey(d => d.DestinyId)
                    .HasConstraintName("FK_Ride_Destiny_DestinyId");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.Ride)
                    .HasForeignKey(d => d.DriverId)
                    .HasConstraintName("FK_Ride_Member_DriverId");
            });
        }
    }
}
