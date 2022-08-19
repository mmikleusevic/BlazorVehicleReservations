using BlazorVehicleReservations.Shared.Models.Dto;
using BlazorVehicleReservations.Shared.Models.Search;
using BlazorVehicleReservations.WEB.Models;

namespace BlazorVehicleReservations.WEB.Services.Interface
{
    public interface IClientService
    {
        Task<List<ClientDto>> GetAllClients();
        Task<ClientDto> GetClient(int id);
        Task DeleteClient(int id);
        Task UpdateClient(ClientDto clientDto, int id);
        Task<List<ClientDto>> SearchClient(ClientSearch clientSearch);
        Task CreateClient(ClientDto clientDto);
    }
}
