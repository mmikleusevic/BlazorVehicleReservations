using BlazorVehicleReservations.Shared.Models.Dto;

namespace BlazorVehicleReservations.API.Service
{
    public interface IVehicleService
    {
        Task<IEnumerable<VehicleDto>> SearchVehicle(VehicleDto vehicle);
        Task<List<VehicleDto>> GetAllVehicles();
        Task<VehicleDto> GetVehicle(int id);
        Task<VehicleDto> CreateVehicle(VehicleDto vehicle);
        Task<VehicleDto> UpdateVehicle(VehicleDto vehicle);
        Task DeleteVehicle(int id);
    }
}
