using BlazorVehicleReservations.API.Service.Interface;
using BlazorVehicleReservations.Shared.Models.Dto;
using BlazorVehicleReservations.Shared.Models.Search;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace BlazorVehicleReservations.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;
        private readonly ILogger<ClientController> _logger;
        public ClientController(
            IClientService clientService,
            ILogger<ClientController> logger)
        {
            _clientService = clientService;
            _logger = logger;
        }

        /// <summary>
        /// Searches clients by parameters
        /// </summary>
        /// <param name="clientSearch"></param>
        /// <returns></returns>
        [HttpPost("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SearchClient(ClientSearch clientSearch)
        {
            try
            {
                var result = await _clientService.SearchClient(clientSearch);
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
        /// Returns all clients
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllClients()
        {
            try
            {
                var result = await _clientService.GetAllClients();
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
        /// Return a client
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetClient(int id)
        {
            try
            {
                if (id > 0)
                {
                    var result = await _clientService.GetClient(id);
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
        /// Creates a client
        /// </summary>
        /// <param name="clientDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateClient(ClientDto clientDto)
        {
            try
            {
                if (clientDto != null)
                {
                    var result = await _clientService.CreateClient(clientDto);
                    if (result == 1)
                    {
                        return Created("", result);
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
        /// Deletes a client
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteClient(int id)
        {
            try
            {
                if (id > 0)
                {
                    var result = await _clientService.DeleteClient(id);
                    if (result == 1)
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
        /// Updates a client
        /// </summary>
        /// <param name="clientDto"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateClient(ClientDto clientDto, int id)
        {
            try
            {
                if (id == clientDto.ClientId)
                {
                    var result = await _clientService.UpdateClient(clientDto);
                    if (result == 1)
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
