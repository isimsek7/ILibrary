using BookInfoApp.Entities;
using BookInfoApp.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

public class AuthController : Controller
{
    //Our user list(database)
    private static List<UserEntity> _users = new List<UserEntity>();
    //We create a hash protector
    private readonly IDataProtector _dataProtector;

    public AuthController(IDataProtectionProvider dataProtectionProvider)
    {
        _dataProtector = dataProtectionProvider.CreateProtector("security");
    }

    //Register
    [HttpGet]
    public IActionResult Register() => View();

    //Check if the model sent by the form is valid or not
    [HttpPost]
    public IActionResult Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
    //If exists, it shows an exception
        var userExists = _users.Any(u => u.Email.ToLower() == model.Email.ToLower());
        if (userExists)
        {
            ModelState.AddModelError("", "User already exists.");
            return View(model);
        }
    //Pass hash
        var hashedPassword = _dataProtector.Protect(model.Password);
    //And send it to user pass
        var newUser = new UserEntity
        {
            Id = _users.Count + 1,
            FullName = model.FullName,
            Email = model.Email.ToLower(),
            Password = hashedPassword,
            PhoneNumber = model.PhoneNumber
        };

        _users.Add(newUser);

        TempData["SuccessMessage"] = "User created successfully!";
    //Sends us to login page
        return RedirectToAction("Login");
    }
    
    [HttpGet]
    public IActionResult Login() => View();


    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = _users.FirstOrDefault(u => u.Email.ToLower() == model.Email.ToLower());
        if (user == null)
        {
            ModelState.AddModelError("", "Invalid email or password.");
            return View(model);
        }
    //Password decription
        var rawPassword = _dataProtector.Unprotect(user.Password);
        if (rawPassword != model.Password)
        {
            ModelState.AddModelError("", "Invalid email or password.");
            return View(model);
        }
    //Login operations
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("FullName", user.FullName)
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}
