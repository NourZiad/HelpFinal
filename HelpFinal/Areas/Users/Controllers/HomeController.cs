using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpFinal.Areas.Users.Controllers
{
    public class HomeController : Controller
    {

        [Area("Users")]
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
