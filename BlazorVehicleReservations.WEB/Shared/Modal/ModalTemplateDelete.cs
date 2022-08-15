using Microsoft.AspNetCore.Components;

namespace BlazorVehicleReservations.WEB.Shared.Modal
{
    public class ModalTemplateDelete : ComponentBase
    {
        protected bool ShowConfirmation { get; set; }

        [Parameter]
        public string Title { get; set; } = "Confirm Delete";

        [Parameter]
        public string Message { get; set; } = "Are you sure you want to delete";

        public void ShowDialog()
        {
            ShowConfirmation = true;
            StateHasChanged();
        }

        [Parameter]
        public EventCallback<bool> ConfirmationChanged { get; set; }

        protected async Task OnConfirmationChange(bool value)
        {
            ShowConfirmation = false;
            await ConfirmationChanged.InvokeAsync(value);
        }
    }
}
