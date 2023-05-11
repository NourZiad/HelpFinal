using HelpFinal.Data;
using Microsoft.AspNetCore.Mvc;

namespace HelpFinal.ViewComponents
{
    public class RequestViewComponent: ViewComponent
    {
        private FinalDbContext db;

        public RequestViewComponent(FinalDbContext _db)
        {
            db = _db;
        }
        public IViewComponentResult Invoke()
        {
            var data = db.Requests.OrderByDescending(x => x.RequestId).Take(3);
            return View(data);
        }
    }
}
