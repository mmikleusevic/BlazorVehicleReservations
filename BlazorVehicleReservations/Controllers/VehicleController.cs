using BlazorVehicleReservations.API.Service;
using BlazorVehicleReservations.Shared.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace BlazorVehicleReservations.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly ILogger<VehicleController> _logger;
        public VehicleController(IVehicleService vehicleService, ILogger<VehicleController> logger)
        {
            _vehicleService = vehicleService;
            _logger = logger;
        }

        /// <summary>
        /// Returns all vehicles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllVehicles()
        {
            try
            {
                var result = await _vehicleService.GetAllVehicles();
                if(result.Any())
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Return a vehicle
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Creates a vehicle
        /// </summary>
        /// <param name="vehicleDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateVehicle(VehicleDto vehicleDto)
        {
            try
            {
                if (vehicleDto != null)
                {
                    var result = await _vehicleService.CreateVehicle(vehicleDto);
                    if (result != 0)
                    {
                        //TODO
                        return Created("da", result);
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


        /// <summary>
        /// Deletes a vehicle
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            try
            {
                if (id != 0)
                {
                    var result = await _vehicleService.DeleteVehicle(id);
                    if(result != 0)
                    {
                        return NoContent();
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


        /// <summary>
        /// Updates a vehicle
        /// </summary>
        /// <param name="vehicleDto"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateVehicle(VehicleDto vehicleDto, int id)
        {
            try
            {
                if (id == vehicleDto.VehicleId)
                {
                    var result = await _vehicleService.UpdateVehicle(vehicleDto);
                    if (result != 0)
                    {
                        return Ok();
                    }
                    else
                    {
                        return NoContent();
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
    }
}
