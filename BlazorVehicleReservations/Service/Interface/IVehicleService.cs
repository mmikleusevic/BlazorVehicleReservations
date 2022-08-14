using BlazorVehicleReservations.Shared.Models.Dto;
using BlazorVehicleReservations.Shared.Models.Search;

namespace BlazorVehicleReservations.API.Service.Interface
{
    public interface IVehicleService
    {
        Task<IEnumerable<VehicleDto>> SearchVehicle(VehicleSearch vehicleSearch);
        Task<List<VehicleDto>> GetAllVehicles();
        Task<List<VehicleDto>> GetAllAvailableVehicles();
        Task<VehicleDto> GetVehicle(int id);
        Task<int> CreateVehicle(VehicleDto vehicleDto);
        Task<int> UpdateVehicle(VehicleDto vehicleDto);
        Task<int> DeleteVehicle(int id);
    }
}
