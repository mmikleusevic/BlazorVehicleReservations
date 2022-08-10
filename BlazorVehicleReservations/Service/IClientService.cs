using BlazorVehicleReservations.Shared.Models.Dto;

namespace BlazorVehicleReservations.API.Service
{
    public interface IClientService
    {
        Task<IEnumerable<ClientDto>> SearchClient(ClientDto client);
        Task<List<ClientDto>> GetAllClients();
        Task<ClientDto> GetClient(int id);
        Task<ClientDto> CreateClient(ClientDto client);
        Task<ClientDto> UpdateClient(ClientDto client);
        Task DeleteClient(int id);
    }
}
