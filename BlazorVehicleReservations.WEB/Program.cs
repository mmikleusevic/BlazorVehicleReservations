using Blazored.Modal;
using Blazored.Toast;
using BlazorVehicleReservations.WEB;
using BlazorVehicleReservations.WEB.Models;
using BlazorVehicleReservations.WEB.Models.Interface;
using BlazorVehicleReservations.WEB.Services;
using BlazorVehicleReservations.WEB.Services.Interface;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { 
    BaseAddress = new Uri("http://localhost:5158/")
});

builder.Services.AddBlazoredModal();
builder.Services.AddBlazoredToast();

builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IToastPopup, ToastPopup>();

await builder.Build().RunAsync();
