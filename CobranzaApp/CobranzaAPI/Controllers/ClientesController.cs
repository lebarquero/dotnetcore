using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CobranzaAPI.Core.Interfaces;
using CobranzaAPI.Core.DTOs;

namespace CobranzaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _modelService;

        public ClientesController(IClienteService clienteService) => _modelService = clienteService;

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> GetClientes(string criteria)
        {
            var model = await _modelService.ListAsync(criteria);
            return model.ToList();
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDTO>> GetCliente(int id)
        {
            var model = await _modelService.GetByIdAsync(id);
            return model;
        }

        // POST: api/Clientes
        [HttpPost]
        public async Task<ActionResult<ClienteDTO>> PostCliente(ClienteDTO model)
        {
            await _modelService.AddAsync(model);
            return CreatedAtAction(nameof(GetCliente), new { id = model.IdCliente }, model);
        }

        // PUT: api/Clientes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, ClienteDTO model)
        {
            if (id == model.IdCliente)
            {
                await _modelService.UpdateAsync(model);
                return NoContent();
            }

            return BadRequest();
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            await _modelService.DeleteAsync(id);
            return NoContent();
        }
    }
}
