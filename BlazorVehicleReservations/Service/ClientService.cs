using BlazorVehicleReservations.Shared.Models.Dto;

namespace BlazorVehicleReservations.API.Service
{
    public class ClientService : IClientService
    {
        public Task<int> CreateClient(ClientDto clientDto)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteClient(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ClientDto>> GetAllClients()
        {
            throw new NotImplementedException();
        }

        public Task<ClientDto> GetClient(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ClientDto>> SearchClient(ClientDto clientDto)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateClient(ClientDto clientDto)
        {
            throw new NotImplementedException();
        }
    }
}
