using AutoMapper;
using BlazorVehicleReservations.API.Context;
using BlazorVehicleReservations.Shared;
using BlazorVehicleReservations.Shared.Models.Dto;
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

            var manufacturer = new SqlParameter("@Manufacturer", vehicle.Manufacturer);
            var model = new SqlParameter("@Model", vehicle.Model);
            var type = new SqlParameter("@Type", vehicle.Type);
            var color = new SqlParameter("@Color", vehicle.Color);
            var year = new SqlParameter("@Year", vehicle.Year);

            return await _context.Database.ExecuteSqlRawAsync($"exec spCreateVehicle @Manufacturer, @Model, @Type, @Color, @Year",
                                                                manufacturer, model, type, color, year);
            
        }

        public async Task<int> DeleteVehicle(int id)
        {
            var vehicleId = new SqlParameter("@VehicleId", id);
            return await _context.Database.ExecuteSqlRawAsync("exec spDeleteVehicle @VehicleId", vehicleId);
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

        public async Task<IEnumerable<VehicleDto>> SearchVehicle(VehicleDto vehicleDto)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateVehicle(VehicleDto vehicleDto)
        {
            var vehicle = _mapper.Map<Vehicle>(vehicleDto);

            var vehicleId = new SqlParameter("@VehicleId", vehicle.Id);
            var manufacturer = new SqlParameter("@Manufacturer", vehicle.Manufacturer);
            var model = new SqlParameter("@Model", vehicle.Model);
            var type = new SqlParameter("@Type", vehicle.Type);
            var color = new SqlParameter("@Color", vehicle.Color);
            var year = new SqlParameter("@Year", vehicle.Year);

            return await _context.Database.ExecuteSqlRawAsync("exec spUpdateVehicle @VehicleId, @Manufacturer, @Model, @Type, @Color, @Year",
                                                                vehicleId, manufacturer, model, type, color, year);
        }
    }
}
