using BlazorVehicleReservations.WEB;
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

builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IReservationService, ReservationService>();

await builder.Build().RunAsync();
