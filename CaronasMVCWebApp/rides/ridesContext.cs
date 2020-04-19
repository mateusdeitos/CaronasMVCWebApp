using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CaronasMVCWebApp.rides
{
    public partial class ridesContext : DbContext
    {
        public ridesContext()
        {
        }

        public ridesContext(DbContextOptions<ridesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Members> Members { get; set; }
        public virtual DbSet<RidePrice> RidePrice { get; set; }
        public virtual DbSet<Rides> Rides { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;port=3306;user=developer;password=123456;database=rides");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Members>(entity =>
            {
                entity.ToTable("members");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Fone)
                    .HasColumnName("fone")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<RidePrice>(entity =>
            {
                entity.ToTable("ride_price");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Destiny)
                    .IsRequired()
                    .HasColumnName("destiny")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Price).HasColumnName("price");
            });

            modelBuilder.Entity<Rides>(entity =>
            {
                entity.HasKey(e => new { e.Date, e.Driver, e.Passenger });

                entity.ToTable("rides");

                entity.HasIndex(e => e.Destiny)
                    .HasName("destinyId_idx");

                entity.HasIndex(e => e.Driver)
                    .HasName("driver_idx");

                entity.HasIndex(e => e.Passenger)
                    .HasName("passengerId_idx");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Driver).HasColumnName("driver");

                entity.Property(e => e.Passenger).HasColumnName("passenger");

                entity.Property(e => e.Destiny).HasColumnName("destiny");

                entity.HasOne(d => d.DestinyNavigation)
                    .WithMany(p => p.Rides)
                    .HasForeignKey(d => d.Destiny)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("destinyId");

                entity.HasOne(d => d.DriverNavigation)
                    .WithMany(p => p.RidesDriverNavigation)
                    .HasForeignKey(d => d.Driver)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("driverId");

                entity.HasOne(d => d.PassengerNavigation)
                    .WithMany(p => p.RidesPassengerNavigation)
                    .HasForeignKey(d => d.Passenger)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("passengerId");
            });
        }
    }
}
