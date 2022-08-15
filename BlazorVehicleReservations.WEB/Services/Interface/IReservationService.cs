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
        Task<ResponseMessage> DeleteReservation(int id);
        Task<ResponseMessage> UpdateReservation(ReservationDto reservationDto, int id);
        Task<MessageResult<List<ReservationDto>>> SearchReservations(ReservationSearch reservationSearch);
        Task<ResponseMessage> CreateReservations(ReservationDto reservationDto);
    }
}
