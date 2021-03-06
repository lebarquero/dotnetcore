using CobranzaAPI.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CobranzaAPI.Persistence.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");

            builder.HasKey(c => c.IdCliente);
            
            builder.Property(c => c.FecRegistro)
                .IsRequired();
            
            builder.Property(c => c.NombreCliente)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(c => c.DireccionCliente)
                .HasMaxLength(300);
            
            builder.Property(c => c.TelefonoCliente)
                .HasMaxLength(100);
            
            builder.Property(c => c.Activo)
                .IsRequired();
        }
    }
}