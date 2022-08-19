using Blazored.Modal;

namespace BlazorVehicleReservations.WEB.Models.Interface
{
    public interface IToastPopup
    {
        void ReturnAppropriateMessageDialog(HttpResponseMessage response);
    }
}
