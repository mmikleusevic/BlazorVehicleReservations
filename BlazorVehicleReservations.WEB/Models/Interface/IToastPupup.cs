using Blazored.Modal;

namespace BlazorVehicleReservations.WEB.Models.Interface
{
    public interface IToastPopup
    {
        Task ReturnAppropriateMessageDialog(ResponseMessage messageResult, BlazoredModalInstance ModalInstance);
        Task ReturnAppropriateMessageMain(string message, int statusCode);
    }
}
