using Blazored.Modal;
using Blazored.Toast.Services;
using BlazorVehicleReservations.WEB.Models.Interface;

namespace BlazorVehicleReservations.WEB.Models
{
    public class ToastPopup : IToastPopup
    {
        private readonly IToastService _toastService;
        public ToastPopup(IToastService toastService)
        {
            _toastService = toastService;
        }

        public async Task ReturnAppropriateMessageDialog(ResponseMessage messageResult, BlazoredModalInstance ModalInstance)
        {           
            if (messageResult.StatusCode >= 200 && messageResult.StatusCode < 300 && messageResult.StatusCode != 204)
            {
                _toastService.ShowSuccess(messageResult.Message);
                await ModalInstance.CloseAsync();
            }
            else if (messageResult.StatusCode == 204)
            {
                _toastService.ShowInfo(messageResult.Message);
            }
            else if (messageResult.StatusCode == 404)
            {
                _toastService.ShowWarning(messageResult.Message);
            }
            else
            {
                _toastService.ShowError(messageResult.Message);
            }
        }

        public async Task ReturnAppropriateMessageMain(string message, int statusCode)
        {
            if (statusCode >= 200 && statusCode < 300)
            {
                _toastService.ShowSuccess(message);
            }
            else if (statusCode == 204)
            {
                _toastService.ShowInfo(message);
            }
            else if (statusCode == 404)
            {
                _toastService.ShowWarning(message);
            }
            else
            {
                _toastService.ShowError(message);
            }
            await Task.CompletedTask;
        }
    }
}
