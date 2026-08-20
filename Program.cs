using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SistemaMatriculaURA.Models;

var builder = WebApplication.CreateBuilder(args);

// Base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity con ApplicationUser
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;

    // Se quita el requisito de carácter especial para que la contraseña de
    // las cuentas de demostración (Admin123) sea válida tal como se pidió.
    options.Password.RequireNonAlphanumeric = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

// Permite validar el antiforgery token enviado por cabecera en las peticiones
// AJAX con cuerpo JSON (p. ej. PanelDocente/GuardarAsistencia), donde no existe
// un formulario tradicional del que leer el campo __RequestVerificationToken.
builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "RequestVerificationToken";
});

builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Ruta por defecto → Index (más seguro)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

// Crear roles y usuario Admin por defecto
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        // Crear roles base si no existen
        string[] roles = { "Admin", "Docente", "Estudiante" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // Crear usuario Admin
        var adminEmail = "admin@ura.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,
                FullName = "Administrador del Sistema",
                Carrera = "N/A"
            };

            await userManager.CreateAsync(adminUser, "Admin123");
        }

        // Se asegura el rol Admin en cada arranque, tanto si la cuenta se
        // acaba de crear como si ya existía sin el rol asignado (por ejemplo,
        // si quedó creada de una ejecución anterior antes de este cambio, o
        // si AddToRoleAsync no se llegó a aplicar). Así el login siempre
        // reconoce a admin@ura.com como Admin sin depender de que la cuenta
        // se haya creado "perfecta" la primera vez.
        if (adminUser != null && !await userManager.IsInRoleAsync(adminUser, "Admin"))
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }

        // Cuentas de demostración (los registros correspondientes en las tablas
        // Estudiantes/Docentes ya existen vía HasData en ApplicationDbContext,
        // enlazados por correo). Se crean aquí para que las credenciales publicadas
        // en la documentación funcionen sin pasos manuales adicionales.
        async Task CrearUsuarioDemoAsync(string email, string password, string fullName, string rol, string carrera)
        {
            var usuario = await userManager.FindByEmailAsync(email);
            if (usuario != null) return;

            usuario = new ApplicationUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true,
                FullName = fullName,
                Carrera = carrera
            };

            var resultadoUsuario = await userManager.CreateAsync(usuario, password);
            if (resultadoUsuario.Succeeded)
            {
                await userManager.AddToRoleAsync(usuario, rol);
            }
        }

        await CrearUsuarioDemoAsync("estudiante1@ura.com", "Admin123.", "Nicole Vargas Solano", "Estudiante", "Ingeniería en Sistemas");
        await CrearUsuarioDemoAsync("docente1@ura.com", "Admin123.", "Carlos Brenes Solano", "Docente", "N/A");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Error creando roles/usuarios de demostración al iniciar la aplicación.");
    }
}

app.Run();