using BlazorVehicleReservations.Shared.Models.Dto;
using BlazorVehicleReservations.Shared.Models.Search;

namespace BlazorVehicleReservations.API.Service.Interface
{
    public interface IClientService
    {
        Task<IEnumerable<ClientDto>> SearchClient(ClientSearch clientSearch);
        Task<List<ClientDto>> GetAllClients();
        Task<ClientDto> GetClient(int id);
        Task<int> CreateClient(ClientDto clientDto);
        Task<int> UpdateClient(ClientDto clientDto);
        Task<int> DeleteClient(int id);
    }
}
