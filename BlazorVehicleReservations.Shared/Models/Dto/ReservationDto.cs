using System.ComponentModel.DataAnnotations;

namespace BlazorVehicleReservations.Shared.Models.Dto
{
    public class ReservationDto
    {
        public int Id { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public int VehicleId { get; set; }
        [Required]
        public DateTime ReservedFrom { get; set; }
        [Required]
        public DateTime ReservedUntil { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? Dob { get; set; }
        public string? Gender { get; set; }
        public string? Country { get; set; }
        public string? Manufacturer { get; set; }
        public string? Model { get; set; }
        public string? Type { get; set; }
        public string? Color { get; set; }
        public short? Year { get; set; }
    }
}
