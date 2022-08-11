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
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly ILogger<ReservationController> _logger;
        public ReservationController(
            IReservationService reservationService,
            ILogger<ReservationController> logger)
        {
            _reservationService = reservationService;
            _logger = logger;
        }

        /// <summary>
        /// Returns all reservations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllReservations()
        {
            try
            {
                var result = await _reservationService.GetAllReservations();
                if (result.Any())
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
        /// Return a reservation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetReservation(int id)
        {
            try
            {
                if (id != 0)
                {
                    var result = await _reservationService.GetReservation(id);
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
        /// Creates a reservation
        /// </summary>
        /// <param name="reservationDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateReservation(ReservationDto reservationDto)
        {
            try
            {
                if (reservationDto != null)
                {
                    var result = await _reservationService.CreateReservation(reservationDto);
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
        /// Deletes a reservation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            try
            {
                if (id != 0)
                {
                    var result = await _reservationService.DeleteReservation(id);
                    if (result != 0)
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
        /// Updates a reservation
        /// </summary>
        /// <param name="reservationDto"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateReservation(ReservationDto reservationDto, int id)
        {
            try
            {
                if (id == reservationDto.Id)
                {
                    var result = await _reservationService.UpdateReservation(reservationDto);
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
