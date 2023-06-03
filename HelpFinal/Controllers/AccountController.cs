using HelpFinal.Data;
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



		public AccountController(FinalDbContext context, UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager, RoleManager<IdentityRole> _roleManager)
		{
			this.userManager = _userManager;
			signInManager = _signInManager;
			this.roleManager = _roleManager;
			_context = context;
		}
		#endregion


		public IActionResult Register()
		{
			return View();
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterViewModel model)
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

					if (model.IsVolunteer)
					{
						var volunteer = new UsersVolunteer
						{
							Id = user.Id,
							StudentId = model.StudentId,
							Name = model.Name,
							Email = model.Email,
							Phone = model.Phone,
							Skills = model.Skills
						};

						// Save the volunteer information to the database
						_context.UsersVolunteers.Add(volunteer);
						await _context.SaveChangesAsync();
						var volunteerRoleId = "7A0BD896-23A3-4CF1-9762-64F9C1D2F127";

						var volunteerRole = await roleManager.FindByIdAsync(volunteerRoleId);

						if (volunteerRole != null)
						{
							await userManager.AddToRoleAsync(user, volunteerRole.Name!);

							return RedirectToAction("Login");
						}
					}

					else if (model.IsDisabled)
					{
                        var disabled = new UsersDisabled
                        {
                            Id = user.Id,
                            StudentId = model.StudentId,
                            Name = model.Name,
                            Email = model.Email,
                            Phone = model.Phone,
                            DisabilityType = model.DisabilityType
                        };
                        _context.UsersDisabled.Add(disabled);
                        await _context.SaveChangesAsync();
                        var DisabledRoleId = "9F735A12-7FF8-4EF5-B7D6-B4BE9E5F118A";

                        var DisabledRole = await roleManager.FindByIdAsync(DisabledRoleId);

                        if (DisabledRole != null)
                        {
                            await userManager.AddToRoleAsync(user, DisabledRole.Name!);

                            return RedirectToAction("Login");
                        }
                    }
				
				}
                
            }
            return View();
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
                    var volunteer = new UsersVolunteer
                    {
                        Id = user.Id,
                        StudentId = model.StudentId,
						Name= model.Name,
						Email = model.Email,
						Phone=model.Phone,
						Skills=model.Skills
                    };

                    // Save the volunteer information to the database
                    _context.UsersVolunteers.Add(volunteer);
                    await _context.SaveChangesAsync();
                    var volunteerRoleId = "7A0BD896-23A3-4CF1-9762-64F9C1D2F127";

					var volunteerRole = await roleManager.FindByIdAsync(volunteerRoleId);

					if (volunteerRole != null)
					{
						await userManager.AddToRoleAsync(user, volunteerRole.Name!);

						return RedirectToAction("VolunteerRegistration");
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

                    var disabled = new UsersDisabled
                    {
                        Id = user.Id,
                        StudentId = model.StudentId,
                        Name = model.Name,
                        Email = model.Email,
                        Phone = model.Phone,
                        DisabilityType = model.DisabilityType
                    };
                    _context.UsersDisabled.Add(disabled);
                    await _context.SaveChangesAsync();
                    var DisabledRoleId = "9F735A12-7FF8-4EF5-B7D6-B4BE9E5F118A";

                    var DisabledRole = await roleManager.FindByIdAsync(DisabledRoleId);

                    if (DisabledRole != null)
                    {
                        await userManager.AddToRoleAsync(user, DisabledRole.Name!);

                        return RedirectToAction("NeedHelpRegistration");
                    }
                }

                foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
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
						return RedirectToAction("Index", "Dashboard", new { area = "Administrator" });
					}
					else if (await userManager.IsInRoleAsync(user!, "Disabled"))
					{
						return RedirectToAction("Index", "Home", new { area = "Users" });
					}
                    else if (await userManager.IsInRoleAsync(user!, "Volunteer"))
                    {
                       
                        return RedirectToAction("Index", "Home", new { area = "Volunteer" });
                    }
                }

				ModelState.AddModelError("", "Invalid User or Password");
				return View(model);
			}

			return View(model);
		}

		

		public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Register");
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
