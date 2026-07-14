using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaMatriculaURA.Models;

namespace ProyectoFinal_AlvaradoNicole.Controllers
{
	[Authorize(Roles = "Admin")]
	public class UsuariosController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public UsuariosController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}

		// GET: /Usuarios
		public async Task<IActionResult> Index()
		{
			var usuarios = _userManager.Users.OrderBy(u => u.Email).ToList();
			var lista = new List<UsuarioListItemViewModel>();

			foreach (var u in usuarios)
			{
				var roles = await _userManager.GetRolesAsync(u);
				lista.Add(new UsuarioListItemViewModel
				{
					Id = u.Id,
					Email = u.Email ?? "",
					FullName = u.FullName ?? "",
					Roles = roles.Count > 0 ? string.Join(", ", roles) : "Sin rol"
				});
			}

			return View(lista);
		}

		// GET: /Usuarios/Create
		public IActionResult Create()
		{
			ViewBag.Roles = new SelectList(new[] { "Docente", "Estudiante", "Admin" });
			return View(new CreateUsuarioViewModel());
		}

		// POST: /Usuarios/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateUsuarioViewModel model)
		{
			if (!ModelState.IsValid)
			{
				ViewBag.Roles = new SelectList(new[] { "Docente", "Estudiante", "Admin" });
				return View(model);
			}

			var existente = await _userManager.FindByEmailAsync(model.Email);
			if (existente != null)
			{
				ModelState.AddModelError(string.Empty, "Ya existe un usuario con ese correo.");
				ViewBag.Roles = new SelectList(new[] { "Docente", "Estudiante", "Admin" });
				return View(model);
			}

			var user = new ApplicationUser
			{
				UserName = model.Email,
				Email = model.Email,
				EmailConfirmed = true,
				FullName = model.FullName,
				Carrera = model.Carrera ?? "N/A"
			};

			var result = await _userManager.CreateAsync(user, model.Password);

			if (result.Succeeded)
			{
				if (!await _roleManager.RoleExistsAsync(model.Role))
				{
					await _roleManager.CreateAsync(new IdentityRole(model.Role));
				}

				await _userManager.AddToRoleAsync(user, model.Role);
				TempData["Success"] = $"Usuario {model.Email} creado con rol {model.Role}.";
				return RedirectToAction(nameof(Index));
			}

			foreach (var error in result.Errors)
				ModelState.AddModelError(string.Empty, error.Description);

			ViewBag.Roles = new SelectList(new[] { "Docente", "Estudiante", "Admin" });
			return View(model);
		}

		// POST: /Usuarios/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user != null)
			{
				await _userManager.DeleteAsync(user);
				TempData["Success"] = "Usuario eliminado.";
			}
			return RedirectToAction(nameof(Index));
		}
	}
}