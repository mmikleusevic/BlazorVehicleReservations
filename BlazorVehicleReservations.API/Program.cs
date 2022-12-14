using AutoMapper;
using BlazorVehicleReservations.API.Context;
using BlazorVehicleReservations.API.Mapper;
using BlazorVehicleReservations.API.Service;
using BlazorVehicleReservations.API.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Error()
      .Enrich.FromLogContext()
      .WriteTo.File($@"{Directory.GetCurrentDirectory()}\Logs\log.txt")
      .CreateLogger();

var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new MappingProfile());
});

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<VehicleReservationsContext>(opts => {
    opts.UseSqlServer(
    builder.Configuration["ConnectionStrings:Default"]);
});

builder.Services.AddLogging(loggingBuilder =>
          loggingBuilder.AddSerilog(dispose: true));

var mapper = config.CreateMapper();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BlazorVehicleReservations API",
        Description = "A sample Web API.",
        Contact = new OpenApiContact
        {
            Name = "Marin Mikleušević",
            Email = "mikleusevicmarin@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/marin-mikleušević-80aaaa145/")
        },
        Version = "v1"
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    c.IncludeXmlComments(xmlPath);
});

var origins = "http://localhost:50422;http://localhost:5158;http://localhost:35031;http://localhost:5229".Split(';');

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    builder.WithOrigins(origins)
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials());
});

builder.Services.AddSingleton(mapper);
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IReservationService, ReservationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
