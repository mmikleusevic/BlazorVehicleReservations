using AutoMapper;
using BlazorVehicleReservations.API.Context;
using BlazorVehicleReservations.API.Service.Interface;
using BlazorVehicleReservations.Shared;
using BlazorVehicleReservations.Shared.Models.Dto;
using BlazorVehicleReservations.Shared.Models.Search;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BlazorVehicleReservations.API.Service
{
    public class ClientService : IClientService
    {
        private readonly IMapper _mapper;
        private readonly VehicleReservationsContext _context;
        public ClientService(IMapper mapper,
            VehicleReservationsContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<int> CreateClient(ClientDto clientDto)
        {
            var client = _mapper.Map<Client>(clientDto);

            var firstName = new SqlParameter("@FirstName", client.FirstName);
            var lastName = new SqlParameter("@LastName", client.LastName);
            var dob = new SqlParameter("@Dob", client.Dob);
            var gender = new SqlParameter("@Gender", client.Gender);
            var country = new SqlParameter("@Country", client.Country);

            return await _context.Database.ExecuteSqlRawAsync($"exec spCreateClient @FirstName, @LastName, @Dob, @Gender, @Country",
                                                                firstName, lastName, dob, gender, country);
        }

        public async Task<int> DeleteClient(int id)
        {
            var clientId = new SqlParameter("@ClientId", id);
            return await _context.Database.ExecuteSqlRawAsync("exec spDeleteClient @ClientId", clientId);
        }

        public async Task<List<ClientDto>> GetAllClients()
        {
            var result = await _context.Clients.FromSqlRaw("exec spGetAllClients").ToListAsync();
            return _mapper.Map<List<ClientDto>>(result);
        }

        public async Task<ClientDto> GetClient(int id)
        {
            var clientId = new SqlParameter("@ClientId", id);
            var resultList = await _context.Clients.FromSqlRaw($"exec spGetClient @ClientId", clientId).ToListAsync();
            var result = resultList.FirstOrDefault();
            return _mapper.Map<ClientDto>(result);
        }

        public async Task<IEnumerable<ClientDto>> SearchClient(ClientSearch clientSearch)
        {
            var firstName = new SqlParameter("@FirstName", clientSearch.FirstName);
            var lastName = new SqlParameter("@LastName", clientSearch.LastName);
            var dob = new SqlParameter("@Dob", clientSearch.Dob);
            var gender = new SqlParameter("@Gender", clientSearch.Gender);
            var country = new SqlParameter("@Country", clientSearch.Country);

            var resultList = await _context.Clients.FromSqlRaw($"exec spSearchClient @FirstName, @LastName, @Dob, @Gender, @Country",
                                                                firstName, lastName, dob, gender, country).ToListAsync();

            return _mapper.Map<IEnumerable<ClientDto>>(resultList);
        }

        public async Task<int> UpdateClient(ClientDto clientDto)
        {
            var client = _mapper.Map<Client>(clientDto);

            var clientId = new SqlParameter("@ClientId", client.Id);
            var firstName = new SqlParameter("@FirstName", client.FirstName);
            var lastName = new SqlParameter("@LastName", client.LastName);
            var dob = new SqlParameter("@Dob", client.Dob);
            var gender = new SqlParameter("@Gender", client.Gender);
            var country = new SqlParameter("@Country", client.Country);

            return await _context.Database.ExecuteSqlRawAsync($"exec spUpdateClient @ClientId, @FirstName, @LastName, @Dob, @Gender, @Country",
                                                                clientId, firstName, lastName, dob, gender, country);
        }
    }
}
