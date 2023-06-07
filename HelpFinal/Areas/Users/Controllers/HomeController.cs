using HelpFinal.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HelpFinal.Areas.Users.Controllers
{
    [Area("Users")]
    [Authorize]
    public class HomeController : Controller
    {

     
        private readonly FinalDbContext _context;
       


        public HomeController(FinalDbContext context)
        {
            _context = context;
          
        }
        public IActionResult Index()
        {
            return View();
        }

        
    }
}
