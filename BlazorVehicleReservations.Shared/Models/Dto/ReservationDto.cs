using System.ComponentModel.DataAnnotations;

namespace BlazorVehicleReservations.Shared.Models.Dto
{
    public class ReservationDto
    {
        public int ReservationId { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public int VehicleId { get; set; }
        [Required]
        public DateTime ReservedFrom { get; set; }
        [Required]
        public DateTime ReservedUntil { get; set; }

        public virtual Client Client { get; set; } = null!;
        public virtual Vehicle Vehicle { get; set; } = null!;
    }
}
