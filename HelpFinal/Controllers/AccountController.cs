﻿using HelpFinal.Data;
using HelpFinal.Models;
using HelpFinal.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace HelpFinal.Controllers
{
    public class AccountController : Controller
    {
        #region configuration
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;
        private RoleManager<IdentityRole> roleManager;
        private readonly FinalDbContext _context;

        

        public AccountController(FinalDbContext context,UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager, RoleManager<IdentityRole> _roleManager)
        {
            this.userManager = _userManager;
            signInManager = _signInManager;
            this.roleManager = _roleManager;
            _context = context;
        }
        #endregion


        public IActionResult Register()
        {
            return View("RegistrationType");
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Register(string registrationType)
		{
			if (registrationType == "volunteer")
			{
				return RedirectToAction("VolunteerRegistration");
			}
			else if (registrationType == "needHelp")
			{
				return RedirectToAction("NeedHelpRegistration");
			}
			else
			{
				// Invalid registration type, handle accordingly
				return RedirectToAction("RegistrationType");
			}
		}

		[HttpGet]
		public IActionResult VolunteerRegistration()
		{
			var model = new VolunteerViewModel();
			return View(model);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> VolunteerRegistration(VolunteerViewModel model, [FromServices] UserManager<IdentityUser> userManager, [FromServices] RoleManager<IdentityRole> roleManager)
		{
			if (ModelState.IsValid)
			{
				var user = new IdentityUser
				{
					UserName = model.Email,
					Email = model.Email
				};

				var result = await userManager.CreateAsync(user, model.Password!);

				if (result.Succeeded)
				{
					var volunteerRoleId = "B2511CF5-C58D-41A4-A373-53C8C87096D3";

					var volunteerRole = await roleManager.FindByIdAsync(volunteerRoleId);

					if (volunteerRole != null)
					{
						await userManager.AddToRoleAsync(user, volunteerRole.Name!);

						return RedirectToAction("Login");
					}
				}

				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}

			return View(model);
		}







		[HttpGet]
		public IActionResult NeedHelpRegistration()
		{
			var model = new DisabledViewModel();
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> NeedHelpRegistration(DisabledViewModel model, [FromServices] UserManager<IdentityUser> userManager, [FromServices] RoleManager<IdentityRole> roleManager)
		{
			if (ModelState.IsValid)
			{
				var user = new IdentityUser
				{
					UserName = model.Email,
					Email = model.Email
				};

				var result = await userManager.CreateAsync(user, model.Password!);

				if (result.Succeeded)
				{
					await userManager.AddToRoleAsync(user, "NeedHelp");

					return RedirectToAction("Login");
				}

				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}

			return View(model);
		}




		//public IActionResult Register()
		//{
		//    return View("RegistrationType");
		//}


		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public IActionResult Register(string registrationType)
		//{
		//    if (registrationType == "volunteer")
		//    {
		//        // Redirect to volunteer registration page
		//        return RedirectToAction("VolunteerRegistration");
		//    }
		//    else if (registrationType == "needHelp")
		//    {
		//        // Redirect to need help registration page
		//        return RedirectToAction("NeedHelpRegistration");
		//    }
		//    else
		//    {
		//        // Invalid registration type, handle accordingly
		//        return RedirectToAction("RegistrationType");
		//    }
		//}

		//[HttpGet]
		//public IActionResult VolunteerRegistration()
		//{
		//    // Create a new instance of the VolunteerRegistrationViewModel if needed
		//    var model = new VolunteerRegistrationViewModel();
		//    return View(model);
		//}

		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public IActionResult VolunteerRegistration(VolunteerRegistrationViewModel model)
		//{
		//    if (ModelState.IsValid)
		//    {
		//        // Process the volunteer registration data and redirect to a success page
		//        return RedirectToAction("Login");
		//    }

		//    return View(model);
		//}

		//[HttpGet]
		//public IActionResult NeedHelpRegistration()
		//{
		//    // Create a new instance of the NeedHelpRegistrationViewModel if needed
		//    var model = new NeedHelpRegistrationViewModel();
		//    return View(model);
		//}

		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public IActionResult NeedHelpRegistration(NeedHelpRegistrationViewModel model)
		//{
		//    if (ModelState.IsValid)
		//    {
		//        // Process the need help registration data and redirect to a success page
		//        //return RedirectToAction("NeedHelpPage", "Home");

		//        return RedirectToAction("Login");
		//    }

		//    return View(model);
		//}


		//[HttpPost]
		//public async Task<IActionResult> Register(RegisterViewModel model)
		//{
		//    if (ModelState.IsValid)
		//    {
		//        IdentityUser user = new IdentityUser
		//        {
		//            Email = model.Email,
		//            UserName = model.Email
		//        };
		//        var result = await userManager.CreateAsync(user, model.Password!);
		//        if (result.Succeeded)
		//        {
		//            return RedirectToAction("Login");
		//        }
		//        foreach (var err in result.Errors)
		//        {
		//            ModelState.AddModelError(err.Code, err.Description);
		//        }
		//        return View(model);
		//    }
		//    return View(model);
		//}

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
