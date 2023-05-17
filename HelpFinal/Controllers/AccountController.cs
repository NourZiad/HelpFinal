using HelpFinal.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HelpFinal.Controllers
{
    public class AccountController : Controller
    {
        #region configuration
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;
        private RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager, RoleManager<IdentityRole> _roleManager)
        {
            this.userManager = _userManager;
            signInManager = _signInManager;
            this.roleManager = _roleManager;
        }
        #endregion
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser
                {
                    Email = model.Email,
                    UserName = model.Email
                };
                var result = await userManager.CreateAsync(user, model.Password!);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return View(model);
            }
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email!, model.Password!, false, false);
                if (result.Succeeded)
                {
                    var user = await userManager.FindByEmailAsync(model.Email!);
                    if (await userManager.IsInRoleAsync(user!, "Administrator"))
                    {
                        // Redirect to the dashboard page in the Administrator area for admin users
                        return RedirectToAction("Index", "Dashboard", new { area = "Administrator" });
                    }
                    else if (await userManager.IsInRoleAsync(user!, "Volunteer"))
                    {
                        // Redirect to another page for volunteer users
                        return RedirectToAction("Index", "Home", new { area = "Users" });
                    }
                }

                ModelState.AddModelError("", "Invalid User or Password");
                return View(model);
            }

            return View(model);
        }

        //[HttpPost]
        //public async Task<IActionResult> Login(LoginViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await signInManager.PasswordSignInAsync(model.Email!, model.Password!, false, false);
        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction("Index", "Dashboard", new { area = "Administrator" });
        //        }
        //        ModelState.AddModelError("", "Invalid User or Password");
        //        return View(model);
        //    }
        //    return View(model);
        //}

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        #region Roles
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
        #endregion
    }
}
