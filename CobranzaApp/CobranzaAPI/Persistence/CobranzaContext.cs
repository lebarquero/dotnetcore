using CobranzaAPI.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CobranzaAPI.Persistence
{
    public class CobranzaContext : DbContext
    {
        public CobranzaContext(DbContextOptions<CobranzaContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(CobranzaContext).Assembly);
        }
    }
}