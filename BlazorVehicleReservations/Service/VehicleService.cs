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
        public async Task<VehicleDto> CreateVehicle(VehicleDto vehicleDto)
        {
            var vehicle = _mapper.Map<Vehicle>(vehicleDto);

            var manufacturer = new SqlParameter("@Manufacturer", vehicle.Manufacturer);
            var mode = new SqlParameter("@Model", vehicle.Model);
            var type = new SqlParameter("@Type", vehicle.Type);
            var color = new SqlParameter("@Color", vehicle.Color);
            var year = new SqlParameter("@Year", vehicle.Year);

            var result = _context.Database.ExecuteSqlRaw($"exec createVehicle @Manufacturer, @Model, @Type, @Color, @Year",
                                                                manufacturer, mode, type, color, year);

            return vehicleDto;
        }

        public async Task DeleteVehicle(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<VehicleDto>> GetAllVehicles()
        {
            var result = await _context.Vehicles.FromSqlRaw("exec getAllVehicles").ToListAsync();
            return _mapper.Map<List<VehicleDto>>(result);            
        }

        public async Task<VehicleDto> GetVehicle(int id)
        {
            var vehicleId = new SqlParameter("@VehicleId", id);
            var resultList = await _context.Vehicles.FromSqlRaw($"exec getVehicle @VehicleId", vehicleId).ToListAsync();
            var result = resultList.FirstOrDefault();
            return _mapper.Map<VehicleDto>(result);
        }

        public async Task<IEnumerable<VehicleDto>> SearchVehicle(VehicleDto vehicle)
        {
            throw new NotImplementedException();
        }

        public async Task<VehicleDto> UpdateVehicle(VehicleDto vehicle)
        {
            throw new NotImplementedException();
        }
    }
}
