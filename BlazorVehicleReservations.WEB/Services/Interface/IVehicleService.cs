using BlazorVehicleReservations.Shared.Models;
using BlazorVehicleReservations.Shared.Models.Dto;
using BlazorVehicleReservations.Shared.Models.Search;
using BlazorVehicleReservations.WEB.Models;

namespace BlazorVehicleReservations.WEB.Services.Interface
{
    public interface IVehicleService
    {
        Task<MessageResult<List<VehicleDto>>> GetAllVehicles();
        Task<MessageResult<List<VehicleDto>>> GetAllAvailableVehicles();
        Task<MessageResult<VehicleDto>> GetVehicle(int id);
        Task DeleteVehicle(int id);
        Task UpdateVehicle(VehicleDto vehicleDto, int id);
        Task<MessageResult<List<VehicleDto>>> SearchVehicles(VehicleSearch vehicleSearch);
        Task CreateVehicle(VehicleDto vehicleDto);
    }
}
