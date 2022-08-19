using BlazorVehicleReservations.Shared.Models.Dto;
using BlazorVehicleReservations.Shared.Models.Search;
using BlazorVehicleReservations.WEB.Models;
using BlazorVehicleReservations.WEB.Services.Interface;
using System.Net.Http;
using System.Net;
using System.Text.Json;
using System.Text;
using BlazorVehicleReservations.WEB.Models.Interface;

namespace BlazorVehicleReservations.WEB.Services
{
    public class ClientService : IClientService
    {
        private readonly HttpClient _httpClient;
        private readonly IToastPopup _toastPopup;
        public ClientService(HttpClient httpClient, IToastPopup toastPopup)
        {
            _httpClient = httpClient;
            _toastPopup = toastPopup;
        }

        public async Task CreateClient(ClientDto clientDto)
        {
            HttpContent httpContent = new StringContent(JsonSerializer.Serialize(clientDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"api/Client", httpContent);
            _toastPopup.ReturnAppropriateMessageDialog(response);
        }

        public async Task DeleteClient(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Client/{id}");
            _toastPopup.ReturnAppropriateMessageDialog(response);
        }

        public async Task<MessageResult<List<ClientDto>>> GetAllClients()
        {
            var result = new MessageResult<List<ClientDto>>();
            var response = await _httpClient.GetAsync("api/Client");
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                result.Data = await JsonSerializer.DeserializeAsync<List<ClientDto>>(responseStream);
            }

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
            }

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
            }

            return result;
        }

        public async Task UpdateClient(ClientDto clientDto, int id)
        {
            HttpContent httpContent = new StringContent(JsonSerializer.Serialize(clientDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/Client/{id}", httpContent);
            _toastPopup.ReturnAppropriateMessageDialog(response);
        }
    }
}
