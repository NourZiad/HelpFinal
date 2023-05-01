using HelpFinal.Data;
using Microsoft.AspNetCore.Mvc;

namespace HelpFinal.ViewComponents
{
    public class ContactViewComponent : ViewComponent
    {
            private FinalDbContext db;

            public ContactViewComponent(FinalDbContext _db)
            {
                db = _db;
            }
            public IViewComponentResult Invoke()
            {
                var data = db.Contacts.OrderByDescending(x => x.ContactId);
                return View(data);
            }
        
    }
}
