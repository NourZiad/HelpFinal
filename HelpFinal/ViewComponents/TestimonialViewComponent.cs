using HelpFinal.Data;
using Microsoft.AspNetCore.Mvc;

namespace HelpFinal.ViewComponents
{
    public class TestimonialViewComponent :ViewComponent
    {
        private FinalDbContext db;

        public TestimonialViewComponent(FinalDbContext _db)
        {
            db = _db;
        }
        public IViewComponentResult Invoke()
        {
            var data = db.Testimonials.OrderByDescending(x => x.TestimonialId).Take(3);
            return View(data);
        }
    }
}
