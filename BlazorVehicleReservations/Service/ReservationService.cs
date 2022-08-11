using BlazorVehicleReservations.Shared.Models.Dto;

namespace BlazorVehicleReservations.API.Service
{
    public class ReservationService : IReservationService
    {
        public Task<int> CreateReservation(ReservationDto reservationDto)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteReservation(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ReservationDto>> GetAllReservations()
        {
            throw new NotImplementedException();
        }

        public Task<ReservationDto> GetReservation(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ReservationDto>> SearchReservation(ReservationDto reservationDto)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateReservation(ReservationDto reservationDto)
        {
            throw new NotImplementedException();
        }
    }
}
