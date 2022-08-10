using System.ComponentModel.DataAnnotations;

namespace BlazorVehicleReservations.Shared.Models.Dto
{
    public class VehicleDto
    {
        public int VehicleId { get; set; }
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
