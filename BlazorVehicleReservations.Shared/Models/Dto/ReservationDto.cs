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
        [StringLength(50, ErrorMessage = "First name has to contain maximum of 50 characters")]
        public string? FirstName { get; set; }
        [StringLength(100, ErrorMessage = "Last name has to contain maximum of 100 characters")]
        public string? LastName { get; set; }
        public DateTime? Dob { get; set; }
        [StringLength(50, ErrorMessage = "Gender has to contain maximum of 50 characters")]
        public string? Gender { get; set; }
        [StringLength(100, ErrorMessage = "Country has to contain maximum of 100 characters")]
        public string? Country { get; set; }
        [StringLength(100, ErrorMessage = "Manufacturer has to contain maximum of 100 characters")]
        public string? Manufacturer { get; set; }
        [StringLength(100, ErrorMessage = "Model has to contain maximum of 100 characters")]
        public string? Model { get; set; }
        [StringLength(50, ErrorMessage = "Type has to contain maximum of 50 characters")]
        public string? Type { get; set; }
        [StringLength(50, ErrorMessage = "Color has to contain maximum of 50 characters")]
        public string? Color { get; set; }
        public short? Year { get; set; }
    }
}
