﻿// <auto-generated />
using System;
using BlazorVehicleReservations.API.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BlazorVehicleReservations.API.Migrations
{
    [DbContext(typeof(VehicleReservationsContext))]
    [Migration("20220811074303_StoredProcedure")]
    partial class StoredProcedure
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BlazorVehicleReservations.Shared.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Country")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("Dob")
                        .HasColumnType("date")
                        .HasColumnName("DOB");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Gender")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Client", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Country = "Croatia",
                            Dob = new DateTime(2000, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Marko",
                            Gender = "Male",
                            LastName = "Marulić"
                        },
                        new
                        {
                            Id = 2,
                            Country = "Croatia",
                            Dob = new DateTime(1995, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Ivo",
                            Gender = "Male",
                            LastName = "Ivić"
                        },
                        new
                        {
                            Id = 3,
                            Country = "Croatia",
                            Dob = new DateTime(1980, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Alenko",
                            Gender = "Male",
                            LastName = "Alenić"
                        });
                });

            modelBuilder.Entity("BlazorVehicleReservations.Shared.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReservedFrom")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("ReservedUntil")
                        .HasColumnType("datetime");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("VehicleId")
                        .IsUnique();

                    b.ToTable("Reservation", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ClientId = 1,
                            ReservedFrom = new DateTime(2022, 8, 11, 9, 5, 0, 0, DateTimeKind.Unspecified),
                            ReservedUntil = new DateTime(2022, 8, 12, 9, 5, 0, 0, DateTimeKind.Unspecified),
                            VehicleId = 1
                        },
                        new
                        {
                            Id = 2,
                            ClientId = 1,
                            ReservedFrom = new DateTime(2022, 8, 11, 9, 5, 0, 0, DateTimeKind.Unspecified),
                            ReservedUntil = new DateTime(2022, 8, 12, 9, 5, 0, 0, DateTimeKind.Unspecified),
                            VehicleId = 2
                        });
                });

            modelBuilder.Entity("BlazorVehicleReservations.Shared.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Color")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Manufacturer")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Model")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Type")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<short?>("Year")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.ToTable("Vehicle", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Color = "Black",
                            Manufacturer = "BMW",
                            Model = "Series 3",
                            Type = "Sedan",
                            Year = (short)2010
                        },
                        new
                        {
                            Id = 2,
                            Color = "Red",
                            Manufacturer = "Ferrari",
                            Model = "Maranello",
                            Type = "Limousine",
                            Year = (short)1993
                        },
                        new
                        {
                            Id = 3,
                            Color = "Yellow",
                            Manufacturer = "Lamborghini",
                            Model = "Diablo",
                            Type = "Limousine",
                            Year = (short)1990
                        });
                });

            modelBuilder.Entity("BlazorVehicleReservations.Shared.Reservation", b =>
                {
                    b.HasOne("BlazorVehicleReservations.Shared.Client", "Client")
                        .WithMany("Reservations")
                        .HasForeignKey("ClientId")
                        .IsRequired()
                        .HasConstraintName("FK_Reservation_Client");

                    b.HasOne("BlazorVehicleReservations.Shared.Vehicle", "Vehicle")
                        .WithOne("Reservation")
                        .HasForeignKey("BlazorVehicleReservations.Shared.Reservation", "VehicleId")
                        .IsRequired()
                        .HasConstraintName("FK_Reservation_Vehicle");

                    b.Navigation("Client");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("BlazorVehicleReservations.Shared.Client", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("BlazorVehicleReservations.Shared.Vehicle", b =>
                {
                    b.Navigation("Reservation")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
