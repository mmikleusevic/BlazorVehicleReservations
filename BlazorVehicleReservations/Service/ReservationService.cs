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
            var result = await GetAllClientReservations(reservationDto.ClientId);

            var reservation = _mapper.Map<Reservation>(reservationDto);

            var clientId = new SqlParameter("@ClientId", reservation.ClientId);
            var vehicleId = new SqlParameter("@VehicleId", reservation.VehicleId);
            var reservedFrom = new SqlParameter("@ReservedFrom", reservation.ReservedFrom);
            var reservedUntil = new SqlParameter("@ReservedUntil", reservation.ReservedUntil);

            return await _context.Database.ExecuteSqlRawAsync($"exec spCreateReservation @ClientId, @VehicleId, @ReservedFrom, @ReservedUntil",
                                                                clientId, vehicleId, reservedFrom, reservedUntil);
        }

        public async Task<int> DeleteReservation(int id)
        {
            var reservationId = new SqlParameter("@ReservationId", id);
            return await _context.Database.ExecuteSqlRawAsync("exec spDeleteReservation @ReservationId", reservationId);
        }

        public async Task<List<ReservationDto>> GetAllReservations()
        {
            return await _context.ReservationDtos.FromSqlRaw("exec spGetAllReservations").ToListAsync();
        }

        public async Task<ReservationDto> GetReservation(int id)
        {
            var reservationId = new SqlParameter("@ReservationId", id);
            var resultList = await _context.ReservationDtos.FromSqlRaw($"exec spGetReservation @ReservationId", reservationId).ToListAsync();
            var result = resultList.FirstOrDefault();
            return _mapper.Map<ReservationDto>(result);
        }

        public Task<List<ReservationDto>> GetAllClientReservations(int id)
        {
            var clientId = new SqlParameter("@ClientId", id);
            return Task.FromResult(_context.ReservationDtos.FromSqlRaw("exec spGetAllClientReservations @ClientId", clientId).ToList());
        }

        public async Task<IEnumerable<ReservationDto>> SearchReservation(ReservationDto reservationDto)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateReservation(ReservationDto reservationDto)
        {
            var reservation = _mapper.Map<Reservation>(reservationDto);

            var reservationId = new SqlParameter("@ReservationId", reservation.Id);
            var reservedFrom = new SqlParameter("@ReservedFrom", reservation.ReservedFrom);
            var reservedUntil = new SqlParameter("@ReservedUntil", reservation.ReservedUntil);

            return await _context.Database.ExecuteSqlRawAsync($"exec spUpdateReservation @ReservationId, @ReservedFrom, @ReservedUntil",
                                                                reservationId, reservedFrom, reservedUntil);
        }
    }
}
