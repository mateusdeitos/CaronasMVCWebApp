﻿// <auto-generated />
using System;
using CaronasMVCWebApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CaronasMVCWebApp.Migrations
{
    [DbContext(typeof(CaronasMVCWebAppContext))]
    [Migration("20200421013416_Rides_cost_attribute")]
    partial class Rides_cost_attribute
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CaronasMVCWebApp.Models.Destinies", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("CustoPorPassageiro");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.HasKey("Id");

                    b.ToTable("Destinies");
                });

            modelBuilder.Entity("CaronasMVCWebApp.Models.Members", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Fone");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<int?>("RidesId");

                    b.HasKey("Id");

                    b.HasIndex("RidesId");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("CaronasMVCWebApp.Models.Rides", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Custo");

                    b.Property<DateTime>("Data");

                    b.Property<int?>("DestinoId");

                    b.Property<int?>("MotoristaId");

                    b.HasKey("Id");

                    b.HasIndex("DestinoId");

                    b.HasIndex("MotoristaId");

                    b.ToTable("Rides");
                });

            modelBuilder.Entity("CaronasMVCWebApp.Models.Members", b =>
                {
                    b.HasOne("CaronasMVCWebApp.Models.Rides")
                        .WithMany("Passageiros")
                        .HasForeignKey("RidesId");
                });

            modelBuilder.Entity("CaronasMVCWebApp.Models.Rides", b =>
                {
                    b.HasOne("CaronasMVCWebApp.Models.Destinies", "Destino")
                        .WithMany()
                        .HasForeignKey("DestinoId");

                    b.HasOne("CaronasMVCWebApp.Models.Members", "Motorista")
                        .WithMany()
                        .HasForeignKey("MotoristaId");
                });
#pragma warning restore 612, 618
        }
    }
}
