using CobranzaAPI.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CobranzaAPI.Core.Interfaces
{
    public interface IClienteService
    {
        Task<ClienteDTO> GetByIdAsync(int id = 0);
        Task<IList<ClienteDTO>> ListAsync(string criteria);
        Task<bool> AddAsync(ClienteDTO model);
        Task<bool> UpdateAsync(ClienteDTO model);
        Task<bool> DeleteAsync(int id);
    }
}