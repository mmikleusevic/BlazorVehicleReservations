using AutoMapper;
using BlazorVehicleReservations.API.Context;
using BlazorVehicleReservations.Shared;
using BlazorVehicleReservations.Shared.Models.Dto;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BlazorVehicleReservations.API.Service
{
    public class ReservationService : IReservationService
    {
        private readonly IMapper _mapper;
        private readonly VehicleReservationsContext _context;
        public ReservationService(IMapper mapper,
            VehicleReservationsContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<int> CreateReservation(ReservationDto reservationDto)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteReservation(int id)
        {
            var reservationId = new SqlParameter("@ReservationId", id);
            return await _context.Database.ExecuteSqlRawAsync("exec spDeleteReservation @ReservationId", reservationId);
        }

        public async Task<List<ReservationDto>> GetAllReservations()
        {
            var result = await _context.Reservations.FromSqlRaw("exec spGetAllReservations").ToListAsync();
            return _mapper.Map<List<ReservationDto>>(result);
        }

        public async Task<ReservationDto> GetReservation(int id)
        {
            var reservationId = new SqlParameter("@ReservationId", id);
            var resultList = await _context.Clients.FromSqlRaw($"exec spGetReservation @ReservationId", reservationId).ToListAsync();
            var result = resultList.FirstOrDefault();
            return _mapper.Map<ReservationDto>(result);
        }

        public async Task<IEnumerable<ReservationDto>> SearchReservation(ReservationDto reservationDto)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateReservation(ReservationDto reservationDto)
        {
            throw new NotImplementedException();
        }
    }
}
