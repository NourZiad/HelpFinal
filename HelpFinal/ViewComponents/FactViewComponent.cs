using HelpFinal.Data;
using Microsoft.AspNetCore.Mvc;

namespace HelpFinal.ViewComponents
{
    public class FactViewComponent :ViewComponent
    {
        private FinalDbContext db;

        public FactViewComponent(FinalDbContext _db)
        {
            db = _db;
        }
        public IViewComponentResult Invoke()
        {
            var data = db.Facts.OrderByDescending(x => x.FactId);
            return View(data);
        }
    }
}
