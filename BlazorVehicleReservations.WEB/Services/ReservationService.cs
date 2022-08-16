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
    public class ReservationService : IReservationService
    {
        private readonly HttpClient _httpClient;
        public ReservationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ResponseMessage> CreateReservations(ReservationDto reservationDto)
        {
            HttpContent httpContent = new StringContent(JsonSerializer.Serialize(reservationDto), Encoding.UTF8, "application/json");
            var responseMessage = new ResponseMessage();
            var response = await _httpClient.PostAsync($"api/Reservation", httpContent);
            if (response.StatusCode == HttpStatusCode.Created)
            {
                responseMessage.Message = "Reservation succesfully created";

            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                responseMessage.Message = "Reservation could not be created";
            }
            else
            {
                responseMessage.Message = "Server error occured";
            }
            responseMessage.StatusCode = (int)response.StatusCode;
            return responseMessage;
        }

        public async Task<ResponseMessage> DeleteReservation(int id)
        {
            var responseMessage = new ResponseMessage();
            var response = await _httpClient.DeleteAsync($"api/Reservation/{id}");
            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                responseMessage.Message = "Reservation succesfully deleted";

            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                responseMessage.Message = "Reservation not found";
            }
            else
            {
                responseMessage.Message = "Server error occured";
            }
            responseMessage.StatusCode = (int)response.StatusCode;
            return responseMessage;
        }

        public async Task<MessageResult<List<ReservationDto>>> GetAllCurrentReservations()
        {
            var result = new MessageResult<List<ReservationDto>>();
            var response = await _httpClient.GetAsync("api/Reservation/current");
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                result.Data = await JsonSerializer.DeserializeAsync<List<ReservationDto>>(responseStream);
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

        public async Task<MessageResult<List<ReservationDto>>> GetAllReservations()
        {
            var result = new MessageResult<List<ReservationDto>>();
            var response = await _httpClient.GetAsync("api/Reservation");
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                result.Data = await JsonSerializer.DeserializeAsync<List<ReservationDto>>(responseStream);
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

        public async Task<MessageResult<ReservationDto>> GetReservation(int id)
        {
            var result = new MessageResult<ReservationDto>();
            var response = await _httpClient.GetAsync($"api/Reservation/{id}");
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                result.Data = await JsonSerializer.DeserializeAsync<ReservationDto>(responseStream);
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

        public async Task<MessageResult<List<ReservationDto>>> SearchReservations(ReservationSearch reservationSearch)
        {
            HttpContent httpContent = new StringContent(JsonSerializer.Serialize(reservationSearch), Encoding.UTF8, "application/json");
            var result = new MessageResult<List<ReservationDto>>();
            var response = await _httpClient.PostAsync("api/Reservation/search", httpContent);
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                result.Data = await JsonSerializer.DeserializeAsync<List<ReservationDto>>(responseStream);
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

        public async Task<ResponseMessage> UpdateReservation(ReservationDto reservationDto, int id)
        {
            HttpContent httpContent = new StringContent(JsonSerializer.Serialize(reservationDto), Encoding.UTF8, "application/json");
            var responseMessage = new ResponseMessage();
            var response = await _httpClient.PutAsync($"api/Reservation/{id}", httpContent);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                responseMessage.Message = "Reservation succesfully updated";

            }
            else if (response.StatusCode == HttpStatusCode.NoContent)
            {
                responseMessage.Message = "Reservation was not updated";
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                responseMessage.Message = "Reservation not found";
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
