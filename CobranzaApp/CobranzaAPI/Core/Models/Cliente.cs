using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CobranzaApi.Core.Models
{
    [Table("Clientes")]
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }

        [Required]
        public DateTime FecRegistro { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "La longuitud máxima de caracteres permitida es de 200.")]
        public string NombreCliente { get; set; }

        [StringLength(300, ErrorMessage = "La longuitud máxima de caracteres permitida es de 300.")]
        public string DireccionCliente { get; set; }

        [StringLength(100, ErrorMessage = "La longuitud máxima de caracteres permitida es de 100.")]
        public string TelefonoCliente { get; set; }

        [Required]
        public bool Activo { get; set; }

        // public virtual ICollection<Entrega> Entregas { get; set; }

        // public virtual ICollection<Bien> Bienes { get; set; }

        // public virtual ICollection<Carga> Cargas { get; set; }
    }   
}