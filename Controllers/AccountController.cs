using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SistemaMatriculaURA.Models;

public class AccountController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
