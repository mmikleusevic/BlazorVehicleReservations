using AutoMapper;
using BlazorVehicleReservations.API.Context;
using BlazorVehicleReservations.API.Models;
using BlazorVehicleReservations.API.Service.Interface;
using BlazorVehicleReservations.Shared;
using BlazorVehicleReservations.Shared.Models.Dto;
using BlazorVehicleReservations.Shared.Models.Search;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

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
            var result = await CanVehicleBeReserved(reservationDto);

            if (result)
            {
                var reservation = _mapper.Map<Reservation>(reservationDto);

                var clientId = new SqlParameter("@ClientId", reservation.ClientId);
                var vehicleId = new SqlParameter("@VehicleId", reservation.VehicleId);
                var reservedFrom = new SqlParameter("@ReservedFrom", reservation.ReservedFrom);
                var reservedUntil = new SqlParameter("@ReservedUntil", reservation.ReservedUntil);

                return await _context.Database.ExecuteSqlRawAsync($"exec spCreateReservation @ClientId, @VehicleId, @ReservedFrom, @ReservedUntil",
                                                                    clientId, vehicleId, reservedFrom, reservedUntil);
            }
            return 0;
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

        public async Task<IEnumerable<ReservationDto>> SearchReservation(ReservationSearch reservationSearch)
        {
            var reservedFrom = new SqlParameter("@ReservedFrom", reservationSearch.ReservedFrom);
            var reservedUntil = new SqlParameter("@ReservedUntil", reservationSearch.ReservedUntil);
            var firstName = new SqlParameter("@FirstName", reservationSearch.FirstName);
            var lastName = new SqlParameter("@LastName", reservationSearch.LastName);
            var dob = new SqlParameter("@Dob", reservationSearch.Dob);
            var gender = new SqlParameter("@Gender", reservationSearch.Gender);
            var country = new SqlParameter("@Country", reservationSearch.Country);
            var manufacturer = new SqlParameter("@Manufacturer", reservationSearch.Manufacturer);
            var model = new SqlParameter("@Model", reservationSearch.Model);
            var type = new SqlParameter("@Type", reservationSearch.Type);
            var color = new SqlParameter("@Color", reservationSearch.Color);
            var year = new SqlParameter("@Year", reservationSearch.Year);

            var resultList = await _context.ReservationDtos.FromSqlRaw($"exec spSearchReservation @ReservedFrom, @ReservedUntil, @FirstName, @LastName, @Dob,"+
                                                                    $"@Gender, @Country, @Manufacturer, @Model, @Type, @Color, @Year",
                                                                    reservedFrom, reservedUntil, firstName, lastName, dob, gender, country, manufacturer,
                                                                    model, type, color, year).ToListAsync();

            return _mapper.Map<IEnumerable<ReservationDto>>(resultList);
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

        public async Task<bool> CanVehicleBeReserved(ReservationDto reservationDto)
        {
            var resultList = await GetAllCurrentReservations();

            //Asking is it already reserved and did the client reserve the same type already
            var isReservedOrSameTypeOfVehicle = resultList.Any(a => a.VehicleId == reservationDto.VehicleId 
                                                            || a.Type == reservationDto.Type && a.ClientId == reservationDto.ClientId);

            if (isReservedOrSameTypeOfVehicle)
            {
                return false;
            }

            //Number of cars client currently rents
            var clientNumberOfVehiclesRentedCurrently = resultList.FindAll(a => a.ClientId == reservationDto.ClientId).Count;

            if (clientNumberOfVehiclesRentedCurrently >= 3)
            {
                return false;
            }

            return true;
        }

        public async Task<List<ReservationDto>> GetAllCurrentReservations()
        {
            return await _context.ReservationDtos.FromSqlRaw("exec spGetAllCurrentReservations").ToListAsync();
        }
    }
}
