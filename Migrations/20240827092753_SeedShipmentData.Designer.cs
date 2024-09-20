﻿// <auto-generated />
using System;
using BetaLogistics.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BetaLogistics.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240827092753_SeedShipmentData")]
    partial class SeedShipmentData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BetaLogistics.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            CustomerId = 1,
                            Address = "MAxwell,Calabar",
                            Name = "Wunmi Martins",
                            PhoneNumber = "09043214290"
                        },
                        new
                        {
                            CustomerId = 2,
                            Address = "Iyanapaja,Lagos",
                            Name = "Goke Quadri",
                            PhoneNumber = "080213434370"
                        },
                        new
                        {
                            CustomerId = 3,
                            Address = "Ikeja, Lagos",
                            Name = "Madu Chinedu",
                            PhoneNumber = "08125467893"
                        });
                });

            modelBuilder.Entity("BetaLogistics.Models.Driver", b =>
                {
                    b.Property<int>("DriverId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DriverId"));

                    b.Property<string>("LicenseNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("DriverId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Drivers");

                    b.HasData(
                        new
                        {
                            DriverId = 1,
                            LicenseNumber = "D15445",
                            Name = "Adefemi Adedayo",
                            VehicleId = 3
                        },
                        new
                        {
                            DriverId = 2,
                            LicenseNumber = "D67090",
                            Name = "Azeez Babalola",
                            VehicleId = 2
                        },
                        new
                        {
                            DriverId = 3,
                            LicenseNumber = "B47630",
                            Name = "Uche Phillips",
                            VehicleId = 1
                        });
                });

            modelBuilder.Entity("BetaLogistics.Models.Shipment", b =>
                {
                    b.Property<int>("ShipmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShipmentId"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DriverId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EstimatedDelivery")
                        .HasColumnType("datetime2");

                    b.Property<string>("Origin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrackingNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("ShipmentId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("DriverId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Shipments");

                    b.HasData(
                        new
                        {
                            ShipmentId = 1,
                            CustomerId = 1,
                            Destination = "Port Harcourt",
                            EstimatedDelivery = new DateTime(2024, 8, 30, 2, 27, 52, 611, DateTimeKind.Local).AddTicks(9580),
                            Origin = "Lagos",
                            Status = "In Transit",
                            TrackingNumber = "TRK123456",
                            VehicleId = 1
                        },
                        new
                        {
                            ShipmentId = 2,
                            CustomerId = 2,
                            Destination = "Lagos",
                            EstimatedDelivery = new DateTime(2024, 8, 25, 2, 27, 52, 611, DateTimeKind.Local).AddTicks(9647),
                            Origin = "Calabar",
                            Status = "Delivered",
                            TrackingNumber = "TRK654321",
                            VehicleId = 2
                        },
                        new
                        {
                            ShipmentId = 3,
                            CustomerId = 3,
                            Destination = "Abuja",
                            EstimatedDelivery = new DateTime(2024, 8, 29, 2, 27, 52, 611, DateTimeKind.Local).AddTicks(9651),
                            Origin = "Lagos",
                            Status = "In Transit",
                            TrackingNumber = "TRK654321",
                            VehicleId = 3
                        });
                });

            modelBuilder.Entity("BetaLogistics.Models.Vehicle", b =>
                {
                    b.Property<int>("VehicleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VehicleId"));

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VehicleId");

                    b.ToTable("Vehicles");

                    b.HasData(
                        new
                        {
                            VehicleId = 1,
                            LicensePlate = "BWC823",
                            Model = "Truck"
                        },
                        new
                        {
                            VehicleId = 2,
                            LicensePlate = "XYZ789",
                            Model = "Van"
                        },
                        new
                        {
                            VehicleId = 3,
                            LicensePlate = "DES439",
                            Model = "Hummer Bus"
                        });
                });

            modelBuilder.Entity("BetaLogistics.Models.Driver", b =>
                {
                    b.HasOne("BetaLogistics.Models.Vehicle", "Vehicle")
                        .WithMany("Drivers")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("BetaLogistics.Models.Shipment", b =>
                {
                    b.HasOne("BetaLogistics.Models.Customer", "Customer")
                        .WithMany("Shipments")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BetaLogistics.Models.Driver", null)
                        .WithMany("Shipments")
                        .HasForeignKey("DriverId");

                    b.HasOne("BetaLogistics.Models.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("BetaLogistics.Models.Customer", b =>
                {
                    b.Navigation("Shipments");
                });

            modelBuilder.Entity("BetaLogistics.Models.Driver", b =>
                {
                    b.Navigation("Shipments");
                });

            modelBuilder.Entity("BetaLogistics.Models.Vehicle", b =>
                {
                    b.Navigation("Drivers");
                });
#pragma warning restore 612, 618
        }
    }
}
