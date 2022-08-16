using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlazorVehicleReservations.Shared.Models.Dto
{
    public class VehicleDto
    {
        [JsonPropertyName("VehicleId")]
        public int? VehicleId { get; set; }
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
