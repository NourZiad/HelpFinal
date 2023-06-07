using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HelpFinal.Data;
using HelpFinal.Models;

namespace HelpFinal.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class UsersVolunteersController : Controller
    {
        private readonly FinalDbContext _context;

        public UsersVolunteersController(FinalDbContext context)
        {
            _context = context;
        }

        // GET: Administrator/UsersVolunteers
        public async Task<IActionResult> Index()
        {
              return _context.UsersVolunteers != null ? 
                          View(await _context.UsersVolunteers.ToListAsync()) :
                          Problem("Entity set 'FinalDbContext.UsersVolunteers'  is null.");
        }

        // GET: Administrator/UsersVolunteers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.UsersVolunteers == null)
            {
                return NotFound();
            }

            var usersVolunteer = await _context.UsersVolunteers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usersVolunteer == null)
            {
                return NotFound();
            }

            return View(usersVolunteer);
        }

        // GET: Administrator/UsersVolunteers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrator/UsersVolunteers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,Name,Email,Phone,Skills")] UsersVolunteer usersVolunteer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usersVolunteer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usersVolunteer);
        }

        // GET: Administrator/UsersVolunteers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.UsersVolunteers == null)
            {
                return NotFound();
            }

            var usersVolunteer = await _context.UsersVolunteers.FindAsync(id);
            if (usersVolunteer == null)
            {
                return NotFound();
            }
            return View(usersVolunteer);
        }

        // POST: Administrator/UsersVolunteers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,StudentId,Name,Email,Phone,Skills")] UsersVolunteer usersVolunteer)
        {
            if (id != usersVolunteer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usersVolunteer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersVolunteerExists(usersVolunteer.Id))
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
            return View(usersVolunteer);
        }

        // GET: Administrator/UsersVolunteers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.UsersVolunteers == null)
            {
                return NotFound();
            }

            var usersVolunteer = await _context.UsersVolunteers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usersVolunteer == null)
            {
                return NotFound();
            }

            return View(usersVolunteer);
        }

        // POST: Administrator/UsersVolunteers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.UsersVolunteers == null)
            {
                return Problem("Entity set 'FinalDbContext.UsersVolunteers'  is null.");
            }
            var usersVolunteer = await _context.UsersVolunteers.FindAsync(id);
            if (usersVolunteer != null)
            {
                _context.UsersVolunteers.Remove(usersVolunteer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersVolunteerExists(string id)
        {
          return (_context.UsersVolunteers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
