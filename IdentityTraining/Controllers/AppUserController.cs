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

        [AllowAnonymous]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Save(RegistrationMod registration)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AppUser newUser = new AppUser()
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = registration.Email,
                        Email = registration.Email,
                        Name = registration.Name,
                        Surname = registration.Surname,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = false,
                        TwoFactorEnabled = false,
                        LockoutEnabled = false
                    };

                    IdentityResult Ires = await _userManager.CreateAsync(newUser);

                    if (Ires.Succeeded)
                    {
                        var roleExist = await _roleManager.RoleExistsAsync("User");
                        if (!roleExist)
                        {
                            await _roleManager.CreateAsync(new IdentityRole("User"));
                        }
                        await _userManager.AddToRoleAsync(newUser, "User");
                        return RedirectToAction("Login");
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return View("Register");
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {

                }
            }
            catch
            {

            }
            return View("Login");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
