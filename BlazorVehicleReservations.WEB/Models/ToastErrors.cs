using Blazored.Modal;
using Blazored.Toast.Services;
using BlazorVehicleReservations.WEB.Models.Interface;
using System.Net;

namespace BlazorVehicleReservations.WEB.Models
{
    public class ToastPopup : IToastPopup
    {
        private readonly IToastService _toastService;
        public ToastPopup(IToastService toastService)
        {
            _toastService = toastService;
        }

        public void ReturnAppropriateMessageDialog(HttpResponseMessage? response )
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    _toastService.ShowSuccess(response.ReasonPhrase);
                    break;
                case HttpStatusCode.Created:
                    _toastService.ShowSuccess(response.ReasonPhrase);
                    break;
                case HttpStatusCode.NoContent:
                    _toastService.ShowInfo(response.ReasonPhrase);
                    break;
                case HttpStatusCode.NotFound:
                    _toastService.ShowWarning(response.ReasonPhrase);
                    break;
                case HttpStatusCode.BadRequest:
                    _toastService.ShowWarning(response.ReasonPhrase);
                    break;
                case HttpStatusCode.InternalServerError:
                    _toastService.ShowError(response.ReasonPhrase);
                    break;
            }
        }
    }
}
