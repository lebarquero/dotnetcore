using CobranzaAPI.Core.DTOs;
using CobranzaAPI.Core.Entities;
using CobranzaAPI.Core.Exceptions;
using CobranzaAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
            IList<Cliente> entities;

            if (!string.IsNullOrEmpty(criteria))
            {
                entities = await _entityRepository.ListAsync(c => c.NombreCliente.ToLower().Contains(criteria.ToLower()));
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
            }).ToList();
        }

        public async Task<ClienteDTO> GetByIdAsync(int id = 0)
        {
            if (id == 0)
                return new ClienteDTO();

            var entity = await _entityRepository.GetByIdAsync(id);
            if (entity == null)
                throw new AppNotFoundException();

            return new ClienteDTO {
                IdCliente = entity.IdCliente,
                NombreCliente = entity.NombreCliente,
                DireccionCliente = entity.DireccionCliente,
                TelefonoCliente = entity.TelefonoCliente,
                Activo = entity.Activo
            };
        }

        public async Task<ClienteDTO> AddAsync(ClienteDTO model)
        {
            var entity = await Mapping(model);
            if (entity == null)
                throw new AppNotFoundException();

            await _entityRepository.AddAsync(entity);
            model.IdCliente = entity.IdCliente;

            return model;
        }

        public async Task UpdateAsync(ClienteDTO model)
        {
            var entity = await Mapping(model);
            if (entity == null)
                throw new AppNotFoundException();

            await _entityRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _entityRepository.GetByIdAsync(id);
            if (entity == null)
                throw new AppNotFoundException();

            await _entityRepository.DeleteAsync(entity);
        }

        async Task<Cliente> Mapping(ClienteDTO model)
        {
            Cliente entity;

            if (model.IdCliente == 0)
            {
                entity = new Cliente {
                    FecRegistro = DateTime.Now
                    , Activo = true
                };
            }
            else
            {
                entity = await _entityRepository.GetByIdAsync(model.IdCliente);
                if (entity == null)
                    return null;
                
                entity.Activo = model.Activo;
            }

            entity.NombreCliente = model.NombreCliente;
            entity.DireccionCliente = model.DireccionCliente;
            entity.TelefonoCliente = model.TelefonoCliente;
            
            return entity;
        }
    }
}