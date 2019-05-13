using CobranzaApi.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CobranzaApi.Persistence
{
    public class CobranzaContext : DbContext
    {
        public CobranzaContext(DbContextOptions<CobranzaContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
    }
}