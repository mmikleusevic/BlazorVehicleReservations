using BlazorVehicleReservations.Shared.Models.Dto;
using BlazorVehicleReservations.Shared.Models.Search;
using BlazorVehicleReservations.WEB.Models;
using BlazorVehicleReservations.WEB.Services.Interface;
using System.Net.Http;
using System.Net;
using System.Text.Json;
using System.Text;

namespace BlazorVehicleReservations.WEB.Services
{
    public class ClientService : IClientService
    {
        private readonly HttpClient _httpClient;
        public ClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseMessage> CreateClient(ClientDto clientDto)
        {
            HttpContent httpContent = new StringContent(JsonSerializer.Serialize(clientDto), Encoding.UTF8, "application/json");
            var responseMessage = new ResponseMessage();
            var response = await _httpClient.PostAsync($"api/Client", httpContent);
            if (response.StatusCode == HttpStatusCode.Created)
            {
                responseMessage.Message = "Client succesfully created";
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                responseMessage.Message = "Client could not be created";
            }
            else
            {
                responseMessage.Message = "Server error occured";
            }
            responseMessage.StatusCode = (int)response.StatusCode;
            return responseMessage;
        }

        public async Task<ResponseMessage> DeleteClient(int id)
        {
            var responseMessage = new ResponseMessage();
            var response = await _httpClient.DeleteAsync($"api/Client/{id}");
            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                responseMessage.Message = "Client succesfully deleted";

            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                responseMessage.Message = "Client not found";
            }
            else
            {
                responseMessage.Message = "Server error occured";
            }
            responseMessage.StatusCode = (int)response.StatusCode;
            return responseMessage;
        }

        public async Task<MessageResult<List<ClientDto>>> GetAllClients()
        {
            var result = new MessageResult<List<ClientDto>>();
            var response = await _httpClient.GetAsync("api/Client");
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                result.Data = await JsonSerializer.DeserializeAsync<List<ClientDto>>(responseStream);
                result.Message = "Data succesfully returned";
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                result.Data = null;
                result.Message = "Data not found";
            }
            else
            {
                result.Data = null;
                result.Message = "Server error occured";
            }
            result.StatusCode = (int)response.StatusCode;
            return result;
        }

        public async Task<MessageResult<ClientDto>> GetClient(int id)
        {
            var result = new MessageResult<ClientDto>();
            var response = await _httpClient.GetAsync($"api/Client/{id}");
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                result.Data = await JsonSerializer.DeserializeAsync<ClientDto>(responseStream);
                result.Message = "Data succesfully returned";
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                result.Data = null;
                result.Message = "Data not found";
            }
            else
            {
                result.Data = null;
                result.Message = "Server error occured";
            }
            result.StatusCode = (int)response.StatusCode;
            return result;
        }

        public async Task<MessageResult<List<ClientDto>>> SearchClient(ClientSearch clientSearch)
        {
            HttpContent httpContent = new StringContent(JsonSerializer.Serialize(clientSearch), Encoding.UTF8, "application/json");
            var result = new MessageResult<List<ClientDto>>();
            var response = await _httpClient.PostAsync("api/Client/search", httpContent);
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                result.Data = await JsonSerializer.DeserializeAsync<List<ClientDto>>(responseStream);
                result.Message = "Data succesfully returned";
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                result.Data = null;
                result.Message = "Data not found";
            }
            else
            {
                result.Data = null;
                result.Message = "Server error occured";
            }
            result.StatusCode = (int)response.StatusCode;
            return result;
        }

        public async Task<ResponseMessage> UpdateClient(ClientDto clientDto, int id)
        {
            HttpContent httpContent = new StringContent(JsonSerializer.Serialize(clientDto), Encoding.UTF8, "application/json");
            var responseMessage = new ResponseMessage();
            var response = await _httpClient.PutAsync($"api/Client/{id}", httpContent);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                responseMessage.Message = "Client succesfully updated";

            }
            else if (response.StatusCode == HttpStatusCode.NoContent)
            {
                responseMessage.Message = "Client was not updated";
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                responseMessage.Message = "Client not found";
            }
            else
            {
                responseMessage.Message = "Server error occured";
            }
            responseMessage.StatusCode = (int)response.StatusCode;
            return responseMessage;
        }
    }
}
