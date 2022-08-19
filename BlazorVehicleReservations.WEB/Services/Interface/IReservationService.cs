using BlazorVehicleReservations.Shared.Models.Dto;
using BlazorVehicleReservations.Shared.Models.Search;
using BlazorVehicleReservations.WEB.Models;

namespace BlazorVehicleReservations.WEB.Services.Interface
{
    public interface IReservationService
    {
        Task<List<ReservationDto>> GetAllReservations();
        Task<List<ReservationDto>> GetAllCurrentReservations();
        Task<ReservationDto> GetReservation(int id);
        Task DeleteReservation(int id);
        Task UpdateReservation(ReservationDto reservationDto, int id);
        Task<List<ReservationDto>> SearchReservations(ReservationSearch reservationSearch);
        Task CreateReservations(ReservationDto reservationDto);
    }
}
