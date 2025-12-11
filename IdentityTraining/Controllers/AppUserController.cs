using IdentityTraining.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityTraining.Controllers
{
    public class AppUserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AppUserController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        //REGISTRAZIONE
        [AllowAnonymous]
        public async Task<IActionResult> Registration()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Save(RegistrationMod registration)
        {
            if (!ModelState.IsValid)
            {
                return View("Registration");
            }
            AppUser newUser = new AppUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = registration.Email,
                Email = registration.Email,
                Name = registration.Name,
                Surname = registration.Surname,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                EmailConfirmed = true
            };

            IdentityResult Ires = await _userManager.CreateAsync(newUser, registration.Password);

            if (!Ires.Succeeded)
            {
                foreach (var err in Ires.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                    Console.WriteLine(err.Description);
                }
                return View("Registration", registration);
            }

            if (Ires.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("User"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("User"));
                }
                await _userManager.AddToRoleAsync(newUser, "User");
                return RedirectToAction("Login");
            }

            return View("Registration");
        }

        //LOGIN
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        // Epicode2025
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            AppUser user = await _userManager.FindByEmailAsync(login.Email);

            if (user == null)
            {
                return Content("Email non trovata");
            }

            var res = await _signInManager.PasswordSignInAsync(
                user,
                login.Password,
                isPersistent: true,
                lockoutOnFailure: false
                );

            if (res.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return Content("Password errata");
        }
    }
}
