using BlazorVehicleReservations.API.Service;
using BlazorVehicleReservations.Shared.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BlazorVehicleReservations.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly ILogger<VehicleController> _logger;
        public VehicleController(IVehicleService vehicleService, ILogger<VehicleController> logger)
        {
            _vehicleService = vehicleService;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllVehicles()
        {
            try
            {
                var result = await _vehicleService.GetAllVehicles();
                if(result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            try
            {
                if(id != 0)
                {
                    var result = await _vehicleService.GetVehicle(id);
                    if (result != null)
                    {
                        return Ok(result);
                    }
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> CreateVehicle(VehicleDto vehicle)
        {
            try
            {
                if (vehicle != null)
                {
                    var result = await _vehicleService.CreateVehicle(vehicle);
                    if (result != null)
                    {
                        return Created("/Created",result);
                    }
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }

        }
    }
}
