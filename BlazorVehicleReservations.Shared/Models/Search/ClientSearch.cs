using System.ComponentModel.DataAnnotations;

namespace BlazorVehicleReservations.Shared.Models.Search
{
    public class ClientSearch
    {
        [StringLength(20, ErrorMessage = "You can't search First Name by more than 20 characters")]
        public string? FirstName { get; set; } = string.Empty;
        [StringLength(20, ErrorMessage = "You can't search Last Name by more than 20 characters")]
        public string? LastName { get; set; } = string.Empty;
        [StringLength(10, ErrorMessage = "You can't search DOB by more than 10 characters")]
        public string? Dob { get; set; } = string.Empty;
        [StringLength(20, ErrorMessage = "You can't search Gender by more than 20 characters")]
        public string? Gender { get; set; } = string.Empty;
        [StringLength(20, ErrorMessage = "You can't search Country by more than 20 characters")]
        public string? Country { get; set; } = string.Empty;
    }
}
