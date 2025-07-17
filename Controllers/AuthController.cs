using Microsoft.AspNetCore.Mvc;
using BoutiqueManagement.Data;
using BoutiqueManagement.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

public class AuthController : Controller
{
    private readonly AppDbContext _context;
    public AuthController(AppDbContext context) => _context = context;

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(User user)
    {
        var dbUser = _context.Users.FirstOrDefault(u => u.Username == user.Username && u.PasswordHash == user.PasswordHash);
        if (dbUser != null)
        {
            HttpContext.Session.SetString("Username", dbUser.Username);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, dbUser.Username)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Dashboard", "Boutique");
        }
        ViewBag.Error = "Invalid username or password.";
        return View();
    }

    public async Task<IActionResult> Logout()
    {
        HttpContext.Session.Clear();
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
}
