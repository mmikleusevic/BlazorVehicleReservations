using BlazorVehicleReservations.Shared.Models;
using BlazorVehicleReservations.Shared.Models.Dto;

namespace BlazorVehicleReservations.WEB.Services.Interface
{
    public interface IVehicleService
    {
        Task<MessageResult<List<VehicleDto>>> GetAllVehicles();
        Task<MessageResult<VehicleDto>> GetVehicle(int id);
        Task<MessageResult<VehicleDto>> DeleteVehicle(int id);
    }
}
