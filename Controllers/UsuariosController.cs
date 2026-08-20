using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaMatriculaURA.Models;

namespace ProyectoFinal_AlvaradoNicole.Controllers
{
	[Authorize(Roles = "Admin")]
	public class UsuariosController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly ApplicationDbContext _context;

		public UsuariosController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_context = context;
		}
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
		public IActionResult Create()
		{
			ViewBag.Roles = new SelectList(new[] { "Docente", "Estudiante", "Admin" });
			return View(new CreateUsuarioViewModel());
		}
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
				if (model.Role == "Docente" && !await _context.Docentes.AnyAsync(d => d.Correo == user.Email))
				{
					_context.Docentes.Add(new Docente
					{
						Nombre = user.FullName,
						Correo = user.Email!,
						Especialidad = "Por definir"
					});
					await _context.SaveChangesAsync();
				}
				else if (model.Role == "Estudiante" && !await _context.Estudiantes.AnyAsync(e => e.Correo == user.Email))
				{
					var carrera = await _context.Carreras.FirstOrDefaultAsync();
					if (carrera != null)
					{
						_context.Estudiantes.Add(new Estudiante
						{
							Nombre = user.FullName,
							Correo = user.Email!,
							CarreraId = carrera.Id,
							UserId = user.Id
						});
						await _context.SaveChangesAsync();
					}
				}

				TempData["Success"] = $"Usuario {model.Email} creado con rol {model.Role}.";
				return RedirectToAction(nameof(Index));
			}

			foreach (var error in result.Errors)
				ModelState.AddModelError(string.Empty, error.Description);

			ViewBag.Roles = new SelectList(new[] { "Docente", "Estudiante", "Admin" });
			return View(model);
		}
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