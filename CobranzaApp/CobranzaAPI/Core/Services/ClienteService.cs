using CobranzaAPI.Core.DTOs;
using CobranzaAPI.Core.Entities;
using CobranzaAPI.Core.Exceptions;
using CobranzaAPI.Core.Infrastructure;
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

        public ClienteService(IGenericRepository<Cliente> entityRepository) => _entityRepository = entityRepository;

        public async Task<IList<ClienteDTO>> ListAsync(string criteria)
        {
            IList<Cliente> entities;

            if (string.IsNullOrEmpty(criteria))
            {
                entities = await _entityRepository.ListAsync();
            }
            else
            {
                entities = await _entityRepository.ListAsync(c => c.NombreCliente.ToLower().Contains(criteria.ToLower()));
            }

            return entities.Select(i =>
            {
                return new ClienteDTO
                {
                    IdCliente = i.IdCliente,
                    NombreCliente = i.NombreCliente,
                    DireccionCliente = i.DireccionCliente,
                    TelefonoCliente = i.TelefonoCliente,
                    Activo = i.Activo
                };
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
            var errors = Validate(model).ToList();
            if (errors.Any())
                throw new AppValidationException(errors);
            
            var entity = await Mapping(model);
            if (entity == null)
                throw new AppNotFoundException();

            await _entityRepository.AddAsync(entity);
            model.IdCliente = entity.IdCliente;

            return model;
        }

        public async Task UpdateAsync(ClienteDTO model)
        {
            var errors = Validate(model).ToList();
            if (errors.Any())
                throw new AppValidationException(errors);

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

        IList<ValidationFailure> Validate(ClienteDTO entity)
        {
            var result = new List<ValidationFailure>();

            if (!entity.Activo)
                result.Add(new ValidationFailure("Activo", "El elemento no puede ser editado!"));
            
            if (!entity.TelefonoCliente.StartsWith("505"))
                result.Add(new ValidationFailure("TelefonoCliente", "Solo se permite el código de área 505!"));

            return result;
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