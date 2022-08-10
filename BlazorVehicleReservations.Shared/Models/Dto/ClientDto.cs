using System.ComponentModel.DataAnnotations;

namespace BlazorVehicleReservations.Shared.Models.Dto
{
    public class ClientDto
    {
        public ClientDto()
        {
            Reservations = new HashSet<Reservation>();
        }
        public int ClientId { get; set; }
        [StringLength(50, ErrorMessage = "First name has to contain maximum of 50 characters")]
        public string? FName { get; set; }
        [StringLength(100, ErrorMessage = "Last name has to contain maximum of 100 characters")]
        public string? LName { get; set; }
        public DateTime? Dob { get; set; }
        [StringLength(50, ErrorMessage = "Gender has to contain maximum of 50 characters")]
        public string? Gender { get; set; }
        [StringLength(100, ErrorMessage = "Country has to contain maximum of 100 characters")]
        public string? Country { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
