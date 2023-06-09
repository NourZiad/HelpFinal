﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HelpFinal.Data;
using HelpFinal.Models;
using Microsoft.AspNetCore.Authorization;
using HelpFinal.ViewComponents;
using HelpFinal.Models.ViewModels;
using System.Net.Mail;

namespace HelpFinal.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize]
    public class ContactsController : Controller
    {
        private readonly FinalDbContext _context;

        public ContactsController(FinalDbContext context)
        {
            _context = context;
        }

        // GET: Administrator/Contacts
        public async Task<IActionResult> Index()
        {
              return _context.Contacts != null ? 
                          View(await _context.Contacts.ToListAsync()) :
                          Problem("Entity set 'FinalDbContext.Contacts'  is null.");
        }

        // GET: Administrator/Contacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Contacts == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts
                .FirstOrDefaultAsync(m => m.ContactId == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // GET: Administrator/Contacts/Create
        public IActionResult Create()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Contact(ContactViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            string name = model.Name;
        //            string email = model.Email;
        //            string subject = model.Subject;
        //            string message = model.Message;

        //            string to = "info@example.com"; // Change this email to your recipient email address
        //            string from = email;

        //            MailMessage mail = new MailMessage(from, to);
        //            mail.Subject = $"{subject}: {name}";
        //            mail.Body = $"You have received a new message from your website contact form.\n\n"
        //                        + $"Here are the details:\n\n"
        //                        + $"Name: {name}\n\n"
        //                        + $"Email: {email}\n\n"
        //                        + $"Subject: {subject}\n\n"
        //                        + $"Message: {message}";

        //            SmtpClient smtpClient = new SmtpClient("your-smtp-server");
        //            smtpClient.Send(mail);

        //            return RedirectToAction("ContactSuccess");
        //        }
        //        catch (Exception ex)
        //        {
        //            // Handle any errors or log them
        //            ModelState.AddModelError("", "An error occurred while sending the message.");
        //        }
        //    }

        //    return View(model);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContactViewModel contact)
        {

            if (ModelState.IsValid)
            {
                Contact c = new Contact
                {
                    ContactId = contact.ContactId,
                    Email = contact.Email,
                    Name = contact.Name,
                    Subject = contact.Subject,
                    Message = contact.Message,
                   

                };
                _context.Contacts.Add(c);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return Ok();
        }

        // GET: Administrator/Contacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Contacts == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        // POST: Administrator/Contacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContactId,Name,Email,Subject,Message,CreationDate,IsPublished,IsDeleted,UserId")] Contact contact)
        {
            if (id != contact.ContactId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contact.ContactId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        // GET: Administrator/Contacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Contacts == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts
                .FirstOrDefaultAsync(m => m.ContactId == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Administrator/Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Contacts == null)
            {
                return Problem("Entity set 'FinalDbContext.Contacts'  is null.");
            }
            var contact = await _context.Contacts.FindAsync(id);
            if (contact != null)
            {
                _context.Contacts.Remove(contact);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactExists(int id)
        {
          return (_context.Contacts?.Any(e => e.ContactId == id)).GetValueOrDefault();
        }
        public async Task<IActionResult> SoftDelete(int id)
        {
            var data = _context.Contacts.Find(id);
            if (id != data!.ContactId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    data.IsDeleted = true;
                    _context.Contacts.Update(data);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(data.ContactId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult ContactSuccess()
        {
            return View();
        }

    }
}
