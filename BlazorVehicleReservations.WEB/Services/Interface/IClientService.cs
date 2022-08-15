using BlazorVehicleReservations.Shared.Models.Dto;
using BlazorVehicleReservations.Shared.Models.Search;
using BlazorVehicleReservations.WEB.Models;

namespace BlazorVehicleReservations.WEB.Services.Interface
{
    public interface IClientService
    {
        Task<MessageResult<List<ClientDto>>> GetAllClients();
        Task<MessageResult<ClientDto>> GetClient(int id);
        Task<ResponseMessage> DeleteClient(int id);
        Task<ResponseMessage> UpdateClient(ClientDto clientDto, int id);
        Task<MessageResult<List<ClientDto>>> SearchClient(ClientSearch clientSearch);
        Task<ResponseMessage> CreateClient(ClientDto clientDto);
    }
}
