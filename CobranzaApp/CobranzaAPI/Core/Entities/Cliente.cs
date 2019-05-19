using System;

namespace CobranzaAPI.Core.Entities
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public DateTime FecRegistro { get; set; }
        public string NombreCliente { get; set; }
        public string DireccionCliente { get; set; }
        public string TelefonoCliente { get; set; }
        public bool Activo { get; set; }

        // public virtual ICollection<Entrega> Entregas { get; set; }

        // public virtual ICollection<Bien> Bienes { get; set; }

        // public virtual ICollection<Carga> Cargas { get; set; }
    }   
}