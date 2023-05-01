using HelpFinal.Data;
using Microsoft.AspNetCore.Mvc;

namespace HelpFinal.ViewComponents
{
    public class EventViewComponent : ViewComponent
    {
        private FinalDbContext db;

        public EventViewComponent(FinalDbContext _db)
        {
            db = _db;
        }
        public IViewComponentResult Invoke()
        {
            var data = db.Events.OrderByDescending(x => x.EventId).Take(2);
            return View(data);
        }
    }
}
