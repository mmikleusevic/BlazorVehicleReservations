using BlazorVehicleReservations.Shared.Models.Dto;

namespace BlazorVehicleReservations.API.Service
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationDto>> SearchReservation(ReservationDto reservationDto);
        Task<List<ReservationDto>> GetAllReservations();
        Task<ReservationDto> GetReservation(int id);
        Task<int> CreateReservation(ReservationDto reservationDto);
        Task<int> UpdateReservation(ReservationDto reservationDto);
        Task<int> DeleteReservation(int id);
    }
}
