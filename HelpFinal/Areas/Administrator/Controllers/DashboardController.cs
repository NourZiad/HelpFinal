using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpFinal.Areas.Adminstrator.Controllers
{
    public class DashboardController : Controller
    {
        [Area ("Administrator")]
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
