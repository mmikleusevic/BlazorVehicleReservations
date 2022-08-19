namespace BlazorVehicleReservations.API.Models
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            Reservations = new HashSet<Reservation>();
        }
        public int Id { get; set; }
        public string? Manufacturer { get; set; }
        public string? Model { get; set; }
        public string? Type { get; set; }
        public string? Color { get; set; }
        public short? Year { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
