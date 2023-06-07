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
    public class UsersDisabledsController : Controller
    {
        private readonly FinalDbContext _context;

        public UsersDisabledsController(FinalDbContext context)
        {
            _context = context;
        }

        // GET: Administrator/UsersDisableds
        public async Task<IActionResult> Index()
        {
              return _context.UsersDisabled != null ? 
                          View(await _context.UsersDisabled.ToListAsync()) :
                          Problem("Entity set 'FinalDbContext.UsersDisabled'  is null.");
        }

        // GET: Administrator/UsersDisableds/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.UsersDisabled == null)
            {
                return NotFound();
            }

            var usersDisabled = await _context.UsersDisabled
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usersDisabled == null)
            {
                return NotFound();
            }

            return View(usersDisabled);
        }

        // GET: Administrator/UsersDisableds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrator/UsersDisableds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,Name,Email,Phone,DisabilityType")] UsersDisabled usersDisabled)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usersDisabled);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usersDisabled);
        }

        // GET: Administrator/UsersDisableds/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.UsersDisabled == null)
            {
                return NotFound();
            }

            var usersDisabled = await _context.UsersDisabled.FindAsync(id);
            if (usersDisabled == null)
            {
                return NotFound();
            }
            return View(usersDisabled);
        }

        // POST: Administrator/UsersDisableds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,StudentId,Name,Email,Phone,DisabilityType")] UsersDisabled usersDisabled)
        {
            if (id != usersDisabled.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usersDisabled);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersDisabledExists(usersDisabled.Id))
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
            return View(usersDisabled);
        }

        // GET: Administrator/UsersDisableds/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.UsersDisabled == null)
            {
                return NotFound();
            }

            var usersDisabled = await _context.UsersDisabled
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usersDisabled == null)
            {
                return NotFound();
            }

            return View(usersDisabled);
        }

        // POST: Administrator/UsersDisableds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.UsersDisabled == null)
            {
                return Problem("Entity set 'FinalDbContext.UsersDisabled'  is null.");
            }
            var usersDisabled = await _context.UsersDisabled.FindAsync(id);
            if (usersDisabled != null)
            {
                _context.UsersDisabled.Remove(usersDisabled);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersDisabledExists(string id)
        {
          return (_context.UsersDisabled?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
