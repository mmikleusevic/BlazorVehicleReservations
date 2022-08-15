using System.Diagnostics.CodeAnalysis;

namespace BlazorVehicleReservations.WEB.Models
{
    public class MessageResult<T>
    {
        [NotNull]
        public string? Message { get; set; } = "Loading..."!;
        [AllowNull]
        public T? Data { get; set; }
    }
}
