using BlazorVehicleReservations.Shared.Models.Dto;

namespace BlazorVehicleReservations.API.Service
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationDto>> SearchReservation(ReservationDto reservation);
        Task<List<ReservationDto>> GetAllReservations();
        Task<ReservationDto> GetReservation(int id);
        Task<ReservationDto> CreateReservation(ReservationDto reservation);
        Task<ReservationDto> UpdateReservation(ReservationDto reservation);
        Task DeleteReservation(int id);
    }
}
