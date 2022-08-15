using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlazorVehicleReservations.Shared.Models.Dto
{
    public class ReservationDto
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        [Required]
        [JsonPropertyName("ClientId")]
        public int ClientId { get; set; }
        [Required]
        [JsonPropertyName("VehicleId")]
        public int VehicleId { get; set; }
        [Required]
        [JsonPropertyName("ReservedFrom")]
        public DateTime ReservedFrom { get; set; }
        [Required]
        [JsonPropertyName("ReservedUntil")]
        public DateTime ReservedUntil { get; set; }
        [StringLength(50, ErrorMessage = "First name has to contain maximum of 50 characters")]
        [JsonPropertyName("FirstName")]
        public string? FirstName { get; set; }
        [StringLength(100, ErrorMessage = "Last name has to contain maximum of 100 characters")]
        [JsonPropertyName("LastName")]
        public string? LastName { get; set; }
        [JsonPropertyName("Dob")]
        public DateTime? Dob { get; set; }
        [StringLength(50, ErrorMessage = "Gender has to contain maximum of 50 characters")]
        [JsonPropertyName("Gender")]
        public string? Gender { get; set; }
        [StringLength(100, ErrorMessage = "Country has to contain maximum of 100 characters")]
        [JsonPropertyName("Country")]
        public string? Country { get; set; }
        [StringLength(100, ErrorMessage = "Manufacturer has to contain maximum of 100 characters")]
        [JsonPropertyName("Manufacturer")]
        public string? Manufacturer { get; set; }
        [StringLength(100, ErrorMessage = "Model has to contain maximum of 100 characters")]
        [JsonPropertyName("Model")]
        public string? Model { get; set; }
        [StringLength(50, ErrorMessage = "Type has to contain maximum of 50 characters")]
        [JsonPropertyName("Type")]
        public string? Type { get; set; }
        [StringLength(50, ErrorMessage = "Color has to contain maximum of 50 characters")]
        [JsonPropertyName("Color")]
        public string? Color { get; set; }
        [JsonPropertyName("Year")]
        public short? Year { get; set; }
    }
}
