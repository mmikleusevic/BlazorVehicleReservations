using BlazorVehicleReservations.Shared.Models;
using BlazorVehicleReservations.Shared.Models.Dto;
using BlazorVehicleReservations.Shared.Models.Search;
using BlazorVehicleReservations.WEB.Models;
using BlazorVehicleReservations.WEB.Services.Interface;
using System.Net;
using System.Text;
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

        public async Task<ResponseMessage> DeleteVehicle(int id)
        {
            var responseMessage = new ResponseMessage();
            var response = await _httpClient.DeleteAsync($"api/Vehicle/{id}");
            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                responseMessage.Message = "Vehicle succesfully deleted";

            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                responseMessage.Message = "Vehicle not found";
            }
            else
            {
                responseMessage.Message = "Server error occured";
            }
            responseMessage.StatusCode = (int)response.StatusCode;
            return responseMessage;
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
            result.StatusCode = (int)response.StatusCode;
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
            result.StatusCode = (int)response.StatusCode;
            return result;
        }

        public async Task<MessageResult<List<VehicleDto>>> GetAllAvailableVehicles()
        {
            var result = new MessageResult<List<VehicleDto>>();
            var response = await _httpClient.GetAsync("api/Vehicle/available");
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
            result.StatusCode = (int)response.StatusCode;
            return result;
        }

        public async Task<ResponseMessage> UpdateVehicle(VehicleDto vehicleDto, int id)
        {
            HttpContent httpContent = new StringContent(JsonSerializer.Serialize(vehicleDto), Encoding.UTF8, "application/json");
            var responseMessage = new ResponseMessage();
            var response = await _httpClient.PutAsync($"api/Vehicle/{id}", httpContent);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                responseMessage.Message = "Vehicle succesfully updated";

            }
            else if(response.StatusCode == HttpStatusCode.NoContent)
            {
                responseMessage.Message = "Vehicle didn't update";
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                responseMessage.Message = "Vehicle not found";
            }
            else
            {
                responseMessage.Message = "Server error occured";
            }
            responseMessage.StatusCode = (int)response.StatusCode;
            return responseMessage;
        }

        public async Task<ResponseMessage> CreateVehicle(VehicleDto vehicleDto)
        {
            HttpContent httpContent = new StringContent(JsonSerializer.Serialize(vehicleDto), Encoding.UTF8, "application/json");
            var responseMessage = new ResponseMessage();
            var response = await _httpClient.PostAsync($"api/Vehicle", httpContent);
            if (response.StatusCode == HttpStatusCode.Created)
            {
                responseMessage.Message = "Vehicle succesfully created";
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                responseMessage.Message = "Vehicle could not be created";
            }
            else
            {
                responseMessage.Message = "Server error occured";
            }
            responseMessage.StatusCode = (int)response.StatusCode;
            return responseMessage;
        }

        public async Task<MessageResult<List<VehicleDto>>> SearchVehicles(VehicleSearch vehicleSearch)
        {
            HttpContent httpContent = new StringContent(JsonSerializer.Serialize(vehicleSearch), Encoding.UTF8, "application/json");
            var result = new MessageResult<List<VehicleDto>>();
            var response = await _httpClient.PostAsync("api/Vehicle/search", httpContent);
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
            result.StatusCode = (int)response.StatusCode;
            return result;
        }
    }
}
