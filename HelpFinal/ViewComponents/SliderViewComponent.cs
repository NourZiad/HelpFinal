using HelpFinal.Data;
using Microsoft.AspNetCore.Mvc;

namespace HelpFinal.ViewComponents
{
    public class SliderViewComponent :ViewComponent
    {
        private FinalDbContext db;

        public SliderViewComponent(FinalDbContext _db)
        {
            db = _db;
        }
        public IViewComponentResult Invoke()
        {
            var data = db.Sliders.OrderByDescending(x => x.SliderId).Take(4);
            return View(data);
        }
    }
}
