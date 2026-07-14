using Microsoft.AspNetCore.Identity;

namespace SistemaMatriculaURA.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Carrera { get; set; }
    }
}
