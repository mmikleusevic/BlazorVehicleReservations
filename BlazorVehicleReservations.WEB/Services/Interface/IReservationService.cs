using BlazorVehicleReservations.Shared.Models.Dto;
using BlazorVehicleReservations.Shared.Models.Search;
using BlazorVehicleReservations.WEB.Models;

namespace BlazorVehicleReservations.WEB.Services.Interface
{
    public interface IReservationService
    {
        Task<MessageResult<List<ReservationDto>>> GetAllReservations();
        Task<MessageResult<List<ReservationDto>>> GetAllCurrentReservations();
        Task<MessageResult<ReservationDto>> GetReservation(int id);
        Task DeleteReservation(int id);
        Task UpdateReservation(ReservationDto reservationDto, int id);
        Task<MessageResult<List<ReservationDto>>> SearchReservations(ReservationSearch reservationSearch);
        Task CreateReservations(ReservationDto reservationDto);
    }
}
