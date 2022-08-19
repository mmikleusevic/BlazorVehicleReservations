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
    public class ReservationService : IReservationService
    {
        private readonly HttpClient _httpClient;
        private readonly IToastPopup _toastPopup;
        public ReservationService(HttpClient httpClient, IToastPopup toastPopup)
        {
            _httpClient = httpClient;
            _toastPopup = toastPopup;
        }
        public async Task CreateReservations(ReservationDto reservationDto)
        {
            HttpContent httpContent = new StringContent(JsonSerializer.Serialize(reservationDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"api/Reservation", httpContent);
            _toastPopup.ReturnAppropriateMessageDialog(response);
        }

        public async Task DeleteReservation(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Reservation/{id}");
            _toastPopup.ReturnAppropriateMessageDialog(response);
        }

        public async Task<MessageResult<List<ReservationDto>>> GetAllCurrentReservations()
        {
            var result = new MessageResult<List<ReservationDto>>();
            var response = await _httpClient.GetAsync("api/Reservation/current");
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                result.Data = await JsonSerializer.DeserializeAsync<List<ReservationDto>>(responseStream);
            }

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
            }

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
            }

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
            }

            return result;
        }

        public async Task UpdateReservation(ReservationDto reservationDto, int id)
        {
            HttpContent httpContent = new StringContent(JsonSerializer.Serialize(reservationDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/Reservation/{id}", httpContent);
            _toastPopup.ReturnAppropriateMessageDialog(response);
        }
    }
}
