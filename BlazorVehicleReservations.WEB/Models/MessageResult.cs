using System.Diagnostics.CodeAnalysis;

namespace BlazorVehicleReservations.WEB.Models
{
    public class MessageResult<T>
    {
        [AllowNull]
        public T? Data { get; set; }
    }
}
