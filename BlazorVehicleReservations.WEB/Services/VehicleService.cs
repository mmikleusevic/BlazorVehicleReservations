using BlazorVehicleReservations.Shared.Models;
using BlazorVehicleReservations.Shared.Models.Dto;
using BlazorVehicleReservations.WEB.Services.Interface;
using System.Net;
using System.Text.Json;

namespace BlazorVehicleReservations.WEB.Services
{
    public partial class VehicleService : IVehicleService
    {
        private readonly HttpClient _httpClient;

        public VehicleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<MessageResult<VehicleDto>> DeleteVehicle(int id)
        {
            var result = new MessageResult<VehicleDto>();
            var response = await _httpClient.GetAsync($"api/Vehicle/{id}");
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                result.Data = await JsonSerializer.DeserializeAsync<VehicleDto>(responseStream);
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
            return result;
        }

        public async Task<MessageResult<List<VehicleDto>>> GetAllVehicles()
        {
            var result = new MessageResult<List<VehicleDto>>();
            var response = await _httpClient.GetAsync("api/Vehicle");
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                result.Data = await JsonSerializer.DeserializeAsync<List<VehicleDto>>(responseStream);
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
            return result;
        }

        public async Task<MessageResult<VehicleDto>> GetVehicle(int id)
        {
            var result = new MessageResult<VehicleDto>();
            var response = await _httpClient.GetAsync($"api/Vehicle/{id}");
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                result.Data = await JsonSerializer.DeserializeAsync<VehicleDto>(responseStream);
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
            return result;
        }
    }
}
