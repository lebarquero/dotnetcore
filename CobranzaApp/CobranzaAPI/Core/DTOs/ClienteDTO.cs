using System;
using System.ComponentModel.DataAnnotations;

namespace CobranzaAPI.Core.DTOs
{
    public class ClienteDTO
    {
        [Key]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "El nombre del cliente es requerido.")]
        [StringLength(200, ErrorMessage = "La longuitud máxima de caracteres permitida es de 200.")]
        public string NombreCliente { get; set; }

        [StringLength(300, ErrorMessage = "La longuitud máxima de caracteres permitida es de 300.")]
        public string DireccionCliente { get; set; }

        [StringLength(100, ErrorMessage = "La longuitud máxima de caracteres permitida es de 100.")]
        public string TelefonoCliente { get; set; }

        [Required]
        public bool Activo { get; set; }
    }
}