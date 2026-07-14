using System.ComponentModel.DataAnnotations;

namespace SistemaMatriculaURA.Models
{
    public class CreateUsuarioViewModel
    {
        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El nombre completo es obligatorio")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un rol")]
        public string Role { get; set; }

        public string? Carrera { get; set; }
    }

    public class UsuarioListItemViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Roles { get; set; }
    }
}