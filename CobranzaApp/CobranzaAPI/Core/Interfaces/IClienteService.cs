using CobranzaAPI.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CobranzaAPI.Core.Interfaces
{
    public interface IClienteService
    {
        Task<T> GetByIdAsync(int id = 0);
        Task<IList<ClienteDTO>> ListAsync(string criteria);
        Task<ClienteDTO> AddAsync(ClienteDTO entity);
        Task UpdateAsync(ClienteDTO entity);
        Task DeleteAsync(int id);
    }
}