using HelpFinal.Data;
using Microsoft.AspNetCore.Mvc;

namespace HelpFinal.ViewComponents
{
    public class MenuViewComponent :ViewComponent
    {
        private FinalDbContext db;

        public MenuViewComponent(FinalDbContext _db)
        {
            db = _db;
        }
        public IViewComponentResult Invoke()
        {
            var data = db.Menus.OrderByDescending(x => x.MenuId);
            return View(data);
        }
    }
}
