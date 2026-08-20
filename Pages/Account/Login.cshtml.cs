using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaMatriculaURA.Models;

namespace ProyectoFinal_AlvaradoNicole.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginModel(SignInManager<ApplicationUser> signInManager,
                           UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [BindProperty]
        public LoginViewModel Input { get; set; }

        public void OnGet()
        {
            Input ??= new LoginViewModel();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);

                if (user != null)
                {
                    if (await _userManager.IsInRoleAsync(user, "Admin"))
                        return LocalRedirect("/Admin/Dashboard");

                    if (await _userManager.IsInRoleAsync(user, "Docente"))
                        return LocalRedirect("/PanelDocente/DashboardDocente");

                    if (await _userManager.IsInRoleAsync(user, "Estudiante"))
                        return LocalRedirect("/Home/Dashboard");
                }

                // Fallback si el usuario no tiene rol asignado
                return LocalRedirect("/Home/Dashboard");
            }

            ModelState.AddModelError(string.Empty, "Intento de inicio de sesión inválido.");
            return Page();
        }
    }
}