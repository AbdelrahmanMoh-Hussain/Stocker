using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stocker.Domain.Models;
using Stocker.Web.ViewModels;

namespace Stocker.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,
            RoleManager<IdentityRole<int>> roleManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View(new UserRegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegisterViewModel userRegister)
        {
            if (!ModelState.IsValid)
            {
             
                return View(userRegister);

            }
            AppUser user = new AppUser
            {
                Email = userRegister.Email,
                UserName = userRegister.Email
            };
            var result = await _userManager.CreateAsync(user, userRegister.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(error.Code, error.Description);

                return View(userRegister);
            }
            await _userManager.AddToRoleAsync(user, "Admin");
            await _signInManager.PasswordSignInAsync(user, userRegister.Password, false, false);

            return RedirectToAction(nameof(Index), controllerName: nameof(Stock));

        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginViewModel userLogin)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(userLogin.Email);
                if (user is not null)
                {
                    await _signInManager.SignOutAsync();
                    var result = await _signInManager.PasswordSignInAsync(user, userLogin.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index), controllerName: nameof(Stock));
                    }
                }
                ModelState.AddModelError("", "Invaild Email or Password");
            }
            return View(userLogin);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return View(nameof(Login));
        }

    }
}
