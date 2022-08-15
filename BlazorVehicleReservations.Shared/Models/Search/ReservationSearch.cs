using System.ComponentModel.DataAnnotations;

namespace BlazorVehicleReservations.Shared.Models.Search
{
    public class ReservationSearch
    {
        [StringLength(10, ErrorMessage = "You can't search Reservation From by more than 10 characters")]
        public string? ReservedFrom { get; set; } = string.Empty;
        [StringLength(10, ErrorMessage = "You can't search Reservation Until by more than 10 characters")]
        public string? ReservedUntil { get; set; } = string.Empty;
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
        [StringLength(20, ErrorMessage = "You can't search Manufacturer by more than 20 characters")]
        public string? Manufacturer { get; set; } = string.Empty;
        [StringLength(20, ErrorMessage = "You can't search Model by more than 20 characters")]
        public string? Model { get; set; } = string.Empty;
        [StringLength(20, ErrorMessage = "You can't search Type by more than 20 characters")]
        public string? Type { get; set; } = string.Empty;
        [StringLength(20, ErrorMessage = "You can't search Color by more than 20 characters")]
        public string? Color { get; set; } = string.Empty;
        [StringLength(4, ErrorMessage = "You can't search Year by more than 4 characters")]
        public string? Year { get; set; } = string.Empty;
    }
}
