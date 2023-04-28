using HelpFinal.Data;
using Microsoft.AspNetCore.Mvc;

namespace HelpFinal.ViewComponents
{
    public class UniversityViewComponent : ViewComponent
    {
        private FinalDbContext db;

        public UniversityViewComponent(FinalDbContext _db)
        {
            db = _db;
        }
         public IViewComponentResult Invoke()
        {
            var data = db.Universities.OrderByDescending(x => x.UserId).Take(6);
            return View(data);
        }
    }
}
