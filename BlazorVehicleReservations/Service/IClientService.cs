using BlazorVehicleReservations.Shared.Models.Dto;

namespace BlazorVehicleReservations.API.Service
{
    public interface IClientService
    {
        Task<IEnumerable<ClientDto>> SearchClient(ClientDto clientDto);
        Task<List<ClientDto>> GetAllClients();
        Task<ClientDto> GetClient(int id);
        Task<int> CreateClient(ClientDto clientDto);
        Task<int> UpdateClient(ClientDto clientDto);
        Task<int> DeleteClient(int id);
    }
}
