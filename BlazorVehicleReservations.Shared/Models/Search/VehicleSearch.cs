using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorVehicleReservations.Shared.Models.Search
{
    public class VehicleSearch
    {
        [StringLength(20, ErrorMessage = "You can't search Manufacturer by more than 20 characters")]
        public string? Manufacturer { get; set; }
        [StringLength(20, ErrorMessage = "You can't search Model by more than 20 characters")]
        public string? Model { get; set; }
        [StringLength(20, ErrorMessage = "You can't search Type by more than 20 characters")]
        public string? Type { get; set; }
        [StringLength(20, ErrorMessage = "You can't search Color by more than 20 characters")]
        public string? Color { get; set; }
        [StringLength(4, ErrorMessage = "You can't search Year by more than 10 characters")]
        public string? Year { get; set; }
    }
}
