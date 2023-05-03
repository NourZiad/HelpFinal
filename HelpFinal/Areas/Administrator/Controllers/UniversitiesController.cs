using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HelpFinal.Data;
using HelpFinal.Models;
using Microsoft.AspNetCore.Authorization;

namespace HelpFinal.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize]
    public class UniversitiesController : Controller
    {
        private readonly FinalDbContext _context;

        public UniversitiesController(FinalDbContext context)
        {
            _context = context;
        }

        // GET: Administrator/Universities
        public async Task<IActionResult> Index()
        {
              return _context.Universities != null ? 
                          View(await _context.Universities.ToListAsync()) :
                          Problem("Entity set 'FinalDbContext.Universities'  is null.");
        }

        // GET: Administrator/Universities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Universities == null)
            {
                return NotFound();
            }

            var university = await _context.Universities
                .FirstOrDefaultAsync(m => m.UniversityId == id);
            if (university == null)
            {
                return NotFound();
            }

            return View(university);
        }

        // GET: Administrator/Universities/Create
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UniversityId,UniName,Description,CreationDate,IsPublished,IsDeleted,UserId")] University university)
        {
            if (ModelState.IsValid)
            {
                _context.Add(university);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(university);
        }

        // GET: Administrator/Universities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Universities == null)
            {
                return NotFound();
            }

            var university = await _context.Universities.FindAsync(id);
            if (university == null)
            {
                return NotFound();
            }
            return View(university);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UniversityId,UniName,Description,CreationDate,IsPublished,IsDeleted,UserId")] University university)
        {
            if (id != university.UniversityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(university);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UniversityExists(university.UniversityId))
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
            return View(university);
        }

        // GET: Administrator/Universities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Universities == null)
            {
                return NotFound();
            }

            var university = await _context.Universities
                .FirstOrDefaultAsync(m => m.UniversityId == id);
            if (university == null)
            {
                return NotFound();
            }

            return View(university);
        }

        // POST: Administrator/Universities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Universities == null)
            {
                return Problem("Entity set 'FinalDbContext.Universities'  is null.");
            }
            var university = await _context.Universities.FindAsync(id);
            if (university != null)
            {
                _context.Universities.Remove(university);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UniversityExists(int id)
        {
          return (_context.Universities?.Any(e => e.UniversityId == id)).GetValueOrDefault();
        }
    }
}
