using System.Collections.Generic;
using System.Threading.Tasks;
using CobranzaAPI.Core.Entities;

namespace CobranzaAPI.Core.Interfaces
{
    public interface IRepositoryCliente
    {
        Task<IEnumerable<Cliente>> GetAllClientes();
        Task<Cliente> GetCliente(int id, bool includeRelated = true);
        void Add(Cliente cliente);
        void Remove(Cliente cliente);
    }
}