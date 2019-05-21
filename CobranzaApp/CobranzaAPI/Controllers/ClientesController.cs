using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CobranzaAPI.Core.Entities;
using CobranzaAPI.Persistence;
using CobranzaAPI.Core.Interfaces;
using CobranzaAPI.Core.DTOs;

namespace CobranzaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        // private readonly CobranzaContext _context;
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService) //CobranzaContext context
        {
            // _context = context;
            _clienteService = clienteService;

            // Delete me
            // if (_context.Clientes.Count() == 0)
            // {
            //     _context.Clientes.Add(new Cliente { NombreCliente = "Maria Lopez", FecRegistro = DateTime.Today, Activo = true });
            //     _context.Clientes.Add(new Cliente { NombreCliente = "Jose Perez", FecRegistro = DateTime.Today, Activo = true });
            //     _context.SaveChanges();
            // }
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> GetClientes(string criteria)
        {
            // return await _context.Clientes.ToListAsync();
            return await _clienteService.ListAsync(criteria);
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDTO>> GetCliente(int id)
        {
            // var cliente = await _context.Clientes.FindAsync(id);
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

            // _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                // await _context.SaveChangesAsync();
                await _clienteService.UpdateAsync(model);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Clientes
        [HttpPost]
        public async Task<ActionResult<ClienteDTO>> PostCliente(ClienteDTO model)
        {
            // _context.Clientes.Add(cliente);
            // await _context.SaveChangesAsync();
            await _clienteService.AddAsync(model);

            return CreatedAtAction(nameof(GetCliente), new { id = model.IdCliente }, model);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        // public async Task<ActionResult<ClienteDTO>> DeleteCliente(int id)
        public async Task<IActionResult> DeleteCliente(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            // TODO :: Add a try/catch to control concurrency error
            await _clienteService.DeleteAsync((int)id);

            return NoContent();
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.IdCliente == id);
        }
    }
}
