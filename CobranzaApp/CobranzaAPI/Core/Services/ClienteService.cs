using CobranzaAPI.Core.DTOs;
using CobranzaAPI.Core.Entities;
using CobranzaAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CobranzaAPI.Core.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IGenericRepository<Cliente> _entityRepository;

        public ClienteService(IGenericRepository<Cliente> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public async Task<IList<ClienteDTO>> ListAsync(string criteria)
        {
            Task<IList<Cliente>> entities;

            if (!string.IsNullOrEmpty(criteria))
            {
                entities = await _entityRepository.ListAsync(c => c.Nombre.Contains(criteria));
            }
            else
            {
                entities = await _entityRepository.ListAsync();
            }

            return entities.Select(i => new ClienteDTO {
                IdCliente = i.IdCliente,
                NombreCliente = i.NombreCliente,
                DireccionCliente = i.DireccionCliente,
                TelefonoCliente = i.TelefonoCliente,
                Activo = i.Activo
            });
        }

        public async Task<ClienteDTO> GetByIdAsync(int id = 0)
        {
            if (id == 0)
            {
                return new ClienteDTO();
            }

            var entity = await _entityRepository.GetByIdAsync(id);

            if (entity == null)
            {
                return null;
            }

            return new ClienteDTO {
                IdCliente = entity.IdCliente,
                NombreCliente = entity.NombreCliente,
                DireccionCliente = entity.DireccionCliente,
                TelefonoCliente = entity.TelefonoCliente,
                Activo = entity.Activo
            }
        }

        public async Task<ClienteDTO> AddAsync(ClienteDTO entity)
        {
        }

        public async Task UpdateAsync(ClienteDTO entity)
        {
        }

        public async Task DeleteAsync(int id)
        {
        }
    }
}