using BlazorVehicleReservations.Shared;
using BlazorVehicleReservations.Shared.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace BlazorVehicleReservations.API.Context
{
    public partial class VehicleReservationsContext : DbContext
    {
        public VehicleReservationsContext()
        {
        }

        public VehicleReservationsContext(DbContextOptions<VehicleReservationsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Reservation> Reservations { get; set; } = null!;
        public virtual DbSet<Vehicle> Vehicles { get; set; } = null!;
        public virtual DbSet<ReservationDto> ReservationDtos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:Default");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Client");

                entity.Property(e => e.Country).HasMaxLength(100);

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("DOB");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Gender).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(100);
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.ToTable("Reservation");

                entity.Property(e => e.ReservedFrom).HasColumnType("datetime");

                entity.Property(e => e.ReservedUntil).HasColumnType("datetime");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservation_Client");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservation_Vehicle");
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.ToTable("Vehicle");

                entity.Property(e => e.Color).HasMaxLength(50);

                entity.Property(e => e.Manufacturer).HasMaxLength(100);

                entity.Property(e => e.Model).HasMaxLength(100);

                entity.Property(e => e.Type).HasMaxLength(50);
            });

            modelBuilder.Entity<ReservationDto>(e =>
            {
                e.HasNoKey().ToView(null);
            });

            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    Id = 1,
                    Country = "Croatia",
                    Dob = new DateTime(2000,08,09),
                    FirstName = "Marko",
                    LastName = "Marulić",
                    Gender = "Male"
                },
                new Client
                {
                    Id = 2,
                    Country = "Croatia",
                    Dob = new DateTime(1995, 08, 09),
                    FirstName = "Ivo",
                    LastName = "Ivić",
                    Gender = "Male"
                },
                new Client
                {
                    Id = 3,
                    Country = "Croatia",
                    Dob = new DateTime(1980, 08, 09),
                    FirstName = "Alenko",
                    LastName = "Alenić",
                    Gender = "Male"
                }
            );

            modelBuilder.Entity<Vehicle>().HasData(
                new Vehicle
                {
                    Id = 1,
                    Manufacturer = "BMW",
                    Model = "Series 3",
                    Color = "Black",
                    Type = "Sedan",
                    Year = 2010
                },
                new Vehicle
                {
                    Id = 2,
                    Manufacturer = "Ferrari",
                    Model = "Maranello",
                    Color = "Red",
                    Type = "Limousine",
                    Year = 1993
                },
                new Vehicle
                {
                    Id = 3,
                    Manufacturer = "Lamborghini",
                    Model = "Diablo",
                    Color = "Yellow",
                    Type = "Limousine",
                    Year = 1990
                }
            );

            modelBuilder.Entity<Reservation>().HasData(
                new Reservation
                {
                    Id = 1,
                    ClientId = 1,
                    VehicleId = 1,
                    ReservedFrom = new DateTime(2022, 08, 11, 09, 05, 00),
                    ReservedUntil = new DateTime(2022, 08, 12, 09, 05, 00)
                },
                new Reservation
                {
                    Id = 2,
                    ClientId = 1,
                    VehicleId = 2,
                    ReservedFrom = new DateTime(2022, 08, 11, 09, 05, 00),
                    ReservedUntil = new DateTime(2022, 08, 12, 09, 05, 00)
                }
            );
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
