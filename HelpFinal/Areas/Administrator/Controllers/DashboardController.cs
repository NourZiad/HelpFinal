using Microsoft.AspNetCore.Mvc;

namespace HelpFinal.Areas.Adminstrator.Controllers
{
    public class DashboardController : Controller
    {
        [Area ("Administrator")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
