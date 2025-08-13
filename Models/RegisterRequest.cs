using System;
using System.ComponentModel.DataAnnotations;

namespace ProyectoPROGEND.Models
{
    public class RegisterRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        public string Sexo { get; set; }
    }
}