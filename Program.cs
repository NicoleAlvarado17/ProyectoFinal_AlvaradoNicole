using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SistemaMatriculaURA.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireNonAlphanumeric = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});
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
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        string[] roles = { "Admin", "Docente", "Estudiante" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
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
        if (adminUser != null && !await userManager.IsInRoleAsync(adminUser, "Admin"))
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
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