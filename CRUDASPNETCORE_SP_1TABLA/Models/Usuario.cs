using System.ComponentModel.DataAnnotations;

namespace CRUDASPNETCORE_SP_1TABLA.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        [Required]
        public string? Nombre { get; set; }
        [Required]
        public int Edad { get; set; }
        [Required]
        public string? Correo { get; set; }
    }
}
