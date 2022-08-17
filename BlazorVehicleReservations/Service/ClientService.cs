using AutoMapper;
using BlazorVehicleReservations.API.Context;
using BlazorVehicleReservations.API.Models;
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

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@FirstName", client.FirstName== null ? DBNull.Value : client.FirstName),
                new SqlParameter("@LastName", client.LastName== null ? DBNull.Value : client.LastName),
                new SqlParameter("@Dob", client.Dob== null ? DBNull.Value : client.Dob),
                new SqlParameter("@Gender", client.Gender== null ? DBNull.Value : client.Gender),
                new SqlParameter("@Country", client.Country== null ? DBNull.Value : client.Country)
            };

            return await _context.Database.ExecuteSqlRawAsync($"exec spCreateClient @FirstName, @LastName, @Dob, @Gender, @Country",
                                                                parameters);
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
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@FirstName", clientSearch.FirstName),
                new SqlParameter("@LastName", clientSearch.LastName),
                new SqlParameter("@Dob", clientSearch.Dob),
                new SqlParameter("@Gender", clientSearch.Gender),
                new SqlParameter("@Country", clientSearch.Country)
            };

            var resultList = await _context.Clients.FromSqlRaw($"exec spSearchClient @FirstName, @LastName, @Dob, @Gender, @Country",
                                                                parameters).ToListAsync();

            return _mapper.Map<IEnumerable<ClientDto>>(resultList);
        }

        public async Task<int> UpdateClient(ClientDto clientDto)
        {
            var client = _mapper.Map<Client>(clientDto);

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ClientId", client.Id),
                new SqlParameter("@FirstName", client.FirstName),
                new SqlParameter("@LastName", client.LastName),
                new SqlParameter("@Dob", client.Dob),
                new SqlParameter("@Gender", client.Gender),
                new SqlParameter("@Country", client.Country)
            };

            return await _context.Database.ExecuteSqlRawAsync($"exec spUpdateClient @ClientId, @FirstName, @LastName, @Dob, @Gender, @Country",
                                                                parameters);
        }
    }
}
