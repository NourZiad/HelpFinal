using HelpFinal.Data;
using HelpFinal.Models;
using HelpFinal.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelpFinal.Areas.Volunteer.Controllers
{
    [Area("Volunteer")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly FinalDbContext _context;
        
        public HomeController(FinalDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var requests = _context.StdDisbleds
        .Join(
            _context.UsersDisabled,
            request => request.UserId,
            user => user.Id,
            (request, user) => new RequestViewModel { Request = request, User = user, Accepted = !string.IsNullOrEmpty(request.AcceptedBy) }
        )
        .ToList();

            return View(requests);
        }
        public IActionResult AcceptPosts(int id)
        {
            var request = _context.StdDisbleds.FirstOrDefault(r => r.Id == id);

            if (request != null && string.IsNullOrEmpty(request.AcceptedBy))
            {
                request.AcceptedBy = User.Identity!.Name; // Replace with the actual name of the volunteer
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult RetractPosts(int requestId)
        {
            var request = _context.StdDisbleds.FirstOrDefault(r => r.Id == requestId);

            if (request != null)
            {
                request.AcceptedBy = null;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult AcceptedPosts()
        {
            var acceptedPosts = _context.StdDisbleds.Where(r => !string.IsNullOrEmpty(r.AcceptedBy)).ToList();
            return View(acceptedPosts);
        }


    }
} 
