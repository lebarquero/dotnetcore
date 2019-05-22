using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CobranzaAPI.Core.Interfaces;
using CobranzaAPI.Core.DTOs;

namespace CobranzaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> GetClientes(string criteria)
        {
            var model = await _clienteService.ListAsync(criteria);
            return model.ToList();
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDTO>> GetCliente(int id)
        {
            var model = await _clienteService.GetByIdAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return model;
        }

        // PUT: api/Clientes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, ClienteDTO model)
        {
            if (id != model.IdCliente)
            {
                return BadRequest();
            }

            try
            {
                await _clienteService.UpdateAsync(model);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/Clientes
        [HttpPost]
        public async Task<ActionResult<ClienteDTO>> PostCliente(ClienteDTO model)
        {
            await _clienteService.AddAsync(model);

            return CreatedAtAction(nameof(GetCliente), new { id = model.IdCliente }, model);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            await _clienteService.DeleteAsync((int)id);

            return NoContent();
        }

        // private bool ClienteExists(int id)
        // {
        //     return _context.Clientes.Any(e => e.IdCliente == id);
        // }
    }
}
