using BlazorVehicleReservations.Shared.Models.Dto;
using BlazorVehicleReservations.Shared.Models.Search;

namespace BlazorVehicleReservations.API.Service.Interface
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationDto>> SearchReservation(ReservationSearch reservationSearch);
        Task<List<ReservationDto>> GetAllReservations();
        Task<ReservationDto> GetReservation(int id);
        Task<int> CreateReservation(ReservationDto reservationDto);
        Task<int> UpdateReservation(ReservationDto reservationDto);
        Task<int> DeleteReservation(int id);
    }
}
