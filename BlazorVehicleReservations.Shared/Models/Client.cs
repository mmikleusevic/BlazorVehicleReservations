namespace BlazorVehicleReservations.Shared
{
    public partial class Client
    {
        public Client()
        {
            Reservations = new HashSet<Reservation>();
        }

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? Dob { get; set; }
        public string? Gender { get; set; }
        public string? Country { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
