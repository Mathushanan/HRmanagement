using HRManagement.Domain.DTOs;
using HRManagement.Domain.Entities;
using HRManagement.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            this._clientService = clientService;
        }

        [HttpPost]
        public async Task<IActionResult> AddClient([FromBody] ClientDto clientDto)
        {
            if (clientDto == null)
            {
                return BadRequest("Client data is null!");
            }
            else
            {
                try
                {
                    await _clientService.AddAsync(clientDto);
                    return Ok("Client Added!");
                }

                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            try
            {
                var client = await _clientService.GetByIdAsync(id);
                if (client == null)
                {
                    return NotFound("Client not found!");
                }
                return Ok(client);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            try
            {
                var clientList = await _clientService.GetAllAsync();
                if (clientList == null)
                {
                    return NotFound("Client table is empty!");
                }
                return Ok(clientList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientById(int id)
        {
            try
            {
                var client = await _clientService.DeleteByIdAsync(id);
                if (client == null)
                {
                    return NotFound("Client not found!");
                }
                return Ok(client);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] ClientDto updatedClientDto)
        {
            if (updatedClientDto == null)
            {
                return BadRequest("Client data cannot be null.");
            }

            try
            {
                var result = await _clientService.UpdateAsync(id, updatedClientDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
