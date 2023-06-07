using HelpFinal.Data;
using HelpFinal.Models;
using HelpFinal.Models.ViewModels;
//using MailKit.Net.Smtp;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System.Net.Mail;
using System.Net;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using System.Configuration;
//using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace HelpFinal.Areas.Volunteer.Controllers
{
    [Area("Volunteer")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly FinalDbContext _context;
        private readonly SmtpSettings _smtpSettings;
        private readonly IConfiguration _configuration;
      
        public HomeController(FinalDbContext context, SmtpSettings smtpSettings, IConfiguration configuration)
        {
            _context = context;
            _smtpSettings = smtpSettings;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            var requests = _context.StdDisbleds
                .GroupJoin(
                    _context.UsersDisabled,
                    request => request.UserId,
                    user => user.Id,
                    (request, users) => new { Request = request, Users = users })
                .SelectMany(
                    x => x.Users.DefaultIfEmpty(),
                    (x, user) => new RequestViewModel { Request = x.Request, User = user, Accepted = !string.IsNullOrEmpty(x.Request.AcceptedBy) })
                .ToList();

            return View(requests);
        }

    

        public IActionResult AcceptPosts(int id)
        {
            var request = _context.StdDisbleds.FirstOrDefault(r => r.Id == id);

            if (request != null && string.IsNullOrEmpty(request.AcceptedBy))
            {
                request.AcceptedBy = User.Identity!.Name; 
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

        [HttpPost]
        public IActionResult SendMessage(string requestId, string message)
        {
            var accountSid = "AC130bbe516810bee73212505da0478838";
            var authToken = "292044623e700e41ad8418104f539076";


            TwilioClient.Init(accountSid, authToken);

            var disabledPerson = _context.UsersDisabled.FirstOrDefault(r => r.Phone == requestId);
            var VolunteerPerson = _context.UsersVolunteers.FirstOrDefault(r => r.Phone == requestId);
            if (disabledPerson == null)
            {
                return NotFound();
            }

            var phoneNumber = disabledPerson.Phone;
            var volunteerPhoneNumber = VolunteerPerson.Phone;

            
            MessageResource.Create(
                body: message,
                from: new Twilio.Types.PhoneNumber(volunteerPhoneNumber),
                to: new Twilio.Types.PhoneNumber(phoneNumber)
            );

         
            return RedirectToAction("Index");
        }

    }
} 
