using BlazorVehicleReservations.Shared.Models.Dto;

namespace BlazorVehicleReservations.API.Service
{
    public interface IVehicleService
    {
        Task<IEnumerable<VehicleDto>> SearchVehicle(VehicleDto vehicleDto);
        Task<List<VehicleDto>> GetAllVehicles();
        Task<VehicleDto> GetVehicle(int id);
        Task<int> CreateVehicle(VehicleDto vehicleDto);
        Task<int> UpdateVehicle(VehicleDto vehicleDto);
        Task<int> DeleteVehicle(int id);
    }
}
