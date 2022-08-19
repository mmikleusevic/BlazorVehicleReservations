using Blazored.Toast.Services;
using BlazorVehicleReservations.Shared.Models.Dto;
using BlazorVehicleReservations.Shared.Models.Search;
using BlazorVehicleReservations.WEB.Models;
using BlazorVehicleReservations.WEB.Models.Interface;
using BlazorVehicleReservations.WEB.Services.Interface;
using System.Net;
using System.Text;
using System.Text.Json;

namespace BlazorVehicleReservations.WEB.Services
{
    public partial class VehicleService : IVehicleService
    {
        private readonly HttpClient _httpClient;
        private readonly IToastPopup _toastPopup;

        public VehicleService(HttpClient httpClient, IToastPopup toastPopup)
        {
            _httpClient = httpClient;
            _toastPopup = toastPopup;
        }

        public async Task DeleteVehicle(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Vehicle/{id}");
            _toastPopup.ReturnAppropriateMessageDialog(response);
        }

        public async Task<List<VehicleDto>> GetAllVehicles()
        {
            var result = new List<VehicleDto>();
            var response = await _httpClient.GetAsync("api/Vehicle");
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                result = await JsonSerializer.DeserializeAsync<List<VehicleDto>>(responseStream);
            }

            return result;
        }

        public async Task<VehicleDto> GetVehicle(int id)
        {
            var result = new VehicleDto();
            var response = await _httpClient.GetAsync($"api/Vehicle/{id}");
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                result = await JsonSerializer.DeserializeAsync<VehicleDto>(responseStream);
            }

            return result;
        }

        public async Task<List<VehicleDto>> GetAllAvailableVehicles()
        {
            var result = new List<VehicleDto>();
            var response = await _httpClient.GetAsync("api/Vehicle/available");
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                result = await JsonSerializer.DeserializeAsync<List<VehicleDto>>(responseStream);
            }

            return result;
        }

        public async Task UpdateVehicle(VehicleDto vehicleDto, int id)
        {
            HttpContent httpContent = new StringContent(JsonSerializer.Serialize(vehicleDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/Vehicle/{id}", httpContent);
            _toastPopup.ReturnAppropriateMessageDialog(response);
        }

        public async Task CreateVehicle(VehicleDto vehicleDto)
        {
            HttpContent httpContent = new StringContent(JsonSerializer.Serialize(vehicleDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"api/Vehicle", httpContent);
            _toastPopup.ReturnAppropriateMessageDialog(response);
        }

        public async Task<List<VehicleDto>> SearchVehicles(VehicleSearch vehicleSearch)
        {
            HttpContent httpContent = new StringContent(JsonSerializer.Serialize(vehicleSearch), Encoding.UTF8, "application/json");
            var result = new List<VehicleDto>();
            var response = await _httpClient.PostAsync("api/Vehicle/search", httpContent);
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                result = await JsonSerializer.DeserializeAsync<List<VehicleDto>>(responseStream);
            }

            return result;
        }
    }
}
