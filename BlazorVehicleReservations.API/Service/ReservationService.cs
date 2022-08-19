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

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ClientId", reservation.ClientId),
                    new SqlParameter("@VehicleId", reservation.VehicleId),
                    new SqlParameter("@ReservedFrom", reservation.ReservedFrom),
                    new SqlParameter("@ReservedUntil", reservation.ReservedUntil)
                };

                return await _context.Database.ExecuteSqlRawAsync($"exec spCreateReservation @ClientId, @VehicleId, @ReservedFrom, @ReservedUntil", parameters);
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
            SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ReservedFrom", reservationSearch.ReservedFrom),
                    new SqlParameter("@ReservedUntil", reservationSearch.ReservedUntil),
                    new SqlParameter("@FirstName", reservationSearch.FirstName),
                    new SqlParameter("@LastName", reservationSearch.LastName),
                    new SqlParameter("@Dob", reservationSearch.Dob),
                    new SqlParameter("@Gender", reservationSearch.Gender),
                    new SqlParameter("@Country", reservationSearch.Country),
                    new SqlParameter("@Manufacturer", reservationSearch.Manufacturer),
                    new SqlParameter("@Model", reservationSearch.Model),
                    new SqlParameter("@Type", reservationSearch.Type),
                    new SqlParameter("@Color", reservationSearch.Color),
                    new SqlParameter("@Year", reservationSearch.Year)
                };

            var resultList = await _context.ReservationDtos.FromSqlRaw($"exec spSearchReservation @ReservedFrom, @ReservedUntil, @FirstName, @LastName, @Dob,"+
                                                                    $"@Gender, @Country, @Manufacturer, @Model, @Type, @Color, @Year", parameters).ToListAsync();

            return _mapper.Map<IEnumerable<ReservationDto>>(resultList);
        }

        public async Task<int> UpdateReservation(ReservationDto reservationDto)
        {
            var reservation = _mapper.Map<Reservation>(reservationDto);

            SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ReservationId", reservation.Id),
                    new SqlParameter("@ReservedFrom", reservation.ReservedFrom),
                    new SqlParameter("@ReservedUntil", reservation.ReservedUntil)
                };

            return await _context.Database.ExecuteSqlRawAsync($"exec spUpdateReservation @ReservationId, @ReservedFrom, @ReservedUntil", parameters);
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
