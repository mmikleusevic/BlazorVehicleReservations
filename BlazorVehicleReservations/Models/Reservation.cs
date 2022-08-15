namespace BlazorVehicleReservations.API.Models
{
    public partial class Reservation
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int VehicleId { get; set; }
        public DateTime ReservedFrom { get; set; }
        public DateTime ReservedUntil { get; set; }

        public virtual Client Client { get; set; } = null!;
        public virtual Vehicle Vehicle { get; set; } = null!;
    }
}
