using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlazorVehicleReservations.Shared.Models.Dto
{
    public class ClientDto
    {
        [JsonPropertyName("ClientId")]
        public int? ClientId { get; set; }
        [StringLength(50, ErrorMessage = "First name has to contain maximum of 50 characters")]
        [JsonPropertyName("FName")]
        public string? FName { get; set; }
        [StringLength(100, ErrorMessage = "Last name has to contain maximum of 100 characters")]
        [JsonPropertyName("LName")]
        public string? LName { get; set; }
        [JsonPropertyName("Dob")]
        public DateTime? Dob { get; set; }
        [StringLength(50, ErrorMessage = "Gender has to contain maximum of 50 characters")]
        [JsonPropertyName("Gender")]
        public string? Gender { get; set; }
        [StringLength(100, ErrorMessage = "Country has to contain maximum of 100 characters")]
        [JsonPropertyName("Country")]
        public string? Country { get; set; }
    }
}
