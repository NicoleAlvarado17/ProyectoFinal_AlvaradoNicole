using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaMatriculaURA.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_AlvaradoNicole.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; } = "";

            [Required]
            public string FullName { get; set; } = "";

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; } = "";

            public string Carrera { get; set; } = "";
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var user = new ApplicationUser
            {
                UserName = Input.Email,
                Email = Input.Email,
                FullName = Input.FullName,
                Carrera = Input.Carrera
            };

            var result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                // Determinar rol automáticamente
                var localPart = Input.Email.Split('@')[0].ToLowerInvariant();
                var rol = (localPart.StartsWith("docente") || localPart.StartsWith("profesor"))
                    ? "Docente"
                    : "Estudiante";

                await _userManager.AddToRoleAsync(user, rol);

                // Si es estudiante → crear registro en tabla Estudiantes
                if (rol == "Estudiante")
                {
                    var carrera = await _context.Carreras
                        .FirstOrDefaultAsync(c => c.Nombre == Input.Carrera)
                        ?? await _context.Carreras.FirstOrDefaultAsync();

                    if (carrera != null)
                    {
                        var estudiante = new Estudiante
                        {
                            Nombre = user.FullName,
                            Correo = user.Email,
                            CarreraId = carrera.Id,
                            UserId = user.Id
                        };

                        _context.Estudiantes.Add(estudiante);
                        await _context.SaveChangesAsync();
                    }
                }

                await _signInManager.SignInAsync(user, isPersistent: false);

                return rol == "Docente"
                    ? LocalRedirect("/Home/DashboardDocente")
                    : LocalRedirect("/Home/Dashboard");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return Page();
        }
    }
}
