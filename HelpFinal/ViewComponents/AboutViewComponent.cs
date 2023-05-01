using HelpFinal.Data;
using Microsoft.AspNetCore.Mvc;

namespace HelpFinal.ViewComponents
{
    public class AboutViewComponent :ViewComponent
    {
            private FinalDbContext db;

            public AboutViewComponent(FinalDbContext _db)
            {
                db = _db;
            }
            public IViewComponentResult Invoke()
            {
                var data = db.Abouts.OrderByDescending(x => x.AboutId);
                return View(data);
            }

        
    }
}
