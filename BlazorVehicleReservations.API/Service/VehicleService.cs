using AutoMapper;
using BlazorVehicleReservations.API.Context;
using BlazorVehicleReservations.API.Models;
using BlazorVehicleReservations.API.Service.Interface;
using BlazorVehicleReservations.Shared;
using BlazorVehicleReservations.Shared.Models.Dto;
using BlazorVehicleReservations.Shared.Models.Search;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BlazorVehicleReservations.API.Service
{
    public class VehicleService : IVehicleService
    {
        private readonly IMapper _mapper;
        private readonly VehicleReservationsContext _context;
        public VehicleService(IMapper mapper,
            VehicleReservationsContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<int> CreateVehicle(VehicleDto vehicleDto)
        {
            var vehicle = _mapper.Map<Vehicle>(vehicleDto);

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Manufacturer", vehicle.Manufacturer == null ? DBNull.Value : vehicle.Manufacturer),
                new SqlParameter("@Model", vehicle.Model == null ? DBNull.Value : vehicle.Model),
                new SqlParameter("@Type", vehicle.Type == null ? DBNull.Value : vehicle.Type),
                new SqlParameter("@Color", vehicle.Color == null ? DBNull.Value : vehicle.Color),
                new SqlParameter("@Year", vehicle.Year == null ? DBNull.Value : vehicle.Year)
            };

            return await _context.Database.ExecuteSqlRawAsync($"exec spCreateVehicle @Manufacturer, @Model, @Type, @Color, @Year", parameters);

        }

        public async Task<int> DeleteVehicle(int id)
        {
            var vehicleId = new SqlParameter("@VehicleId", id);
            return await _context.Database.ExecuteSqlRawAsync("exec spDeleteVehicle @VehicleId", vehicleId);
        }

        public async Task<List<VehicleDto>> GetAllAvailableVehicles()
        {
            var result = await _context.Vehicles.FromSqlRaw("exec spGetAllAvailableVehicles").ToListAsync();
            return _mapper.Map<List<VehicleDto>>(result);
        }

        public async Task<List<VehicleDto>> GetAllVehicles()
        {
            var result = await _context.Vehicles.FromSqlRaw("exec spGetAllVehicles").ToListAsync();
            return _mapper.Map<List<VehicleDto>>(result);
        }

        public async Task<VehicleDto> GetVehicle(int id)
        {
            var vehicleId = new SqlParameter("@VehicleId", id);
            var resultList = await _context.Vehicles.FromSqlRaw($"exec spGetVehicle @VehicleId", vehicleId).ToListAsync();
            var result = resultList.FirstOrDefault();
            return _mapper.Map<VehicleDto>(result);
        }

        public async Task<IEnumerable<VehicleDto>> SearchVehicle(VehicleSearch vehicleSearch)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Manufacturer", vehicleSearch.Manufacturer),
                new SqlParameter("@Model", vehicleSearch.Model),
                new SqlParameter("@Type", vehicleSearch.Type),
                new SqlParameter("@Color", vehicleSearch.Color),
                new SqlParameter("@Year", vehicleSearch.Year)
            };

            var resultList = await _context.Vehicles.FromSqlRaw("exec spSearchVehicle @Manufacturer, @Model, @Type, @Color, @Year", parameters).ToListAsync();

            return _mapper.Map<IEnumerable<VehicleDto>>(resultList);
        }

        public async Task<int> UpdateVehicle(VehicleDto vehicleDto)
        {
            var vehicle = _mapper.Map<Vehicle>(vehicleDto);

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@VehicleId", vehicleDto.VehicleId),
                new SqlParameter("@Manufacturer", vehicle.Manufacturer == null ? DBNull.Value : vehicle.Manufacturer),
                new SqlParameter("@Model", vehicle.Model == null ? DBNull.Value : vehicle.Model),
                new SqlParameter("@Type", vehicle.Type == null ? DBNull.Value : vehicle.Type),
                new SqlParameter("@Color", vehicle.Color == null ? DBNull.Value : vehicle.Color),
                new SqlParameter("@Year", vehicle.Year == null ? DBNull.Value : vehicle.Year)
            };

            return await _context.Database.ExecuteSqlRawAsync("exec spUpdateVehicle @VehicleId, @Manufacturer, @Model, @Type, @Color, @Year", parameters);
        }
    }
}
