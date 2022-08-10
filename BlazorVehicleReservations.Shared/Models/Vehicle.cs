namespace BlazorVehicleReservations.Shared
{
    public partial class Vehicle
    {
        public int Id { get; set; }
        public string? Manufacturer { get; set; }
        public string? Model { get; set; }
        public string? Type { get; set; }
        public string? Color { get; set; }
        public short? Year { get; set; }

        public virtual Reservation Reservation { get; set; } = null!;
    }
}
