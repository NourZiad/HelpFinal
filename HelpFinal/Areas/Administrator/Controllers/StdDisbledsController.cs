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
    public class StdDisbledsController : Controller
    {
        private readonly FinalDbContext _context;

        public StdDisbledsController(FinalDbContext context)
        {
            _context = context;
        }

        // GET: Administrator/StdDisbleds
        public async Task<IActionResult> Index()
        {
              return _context.StdDisbleds != null ? 
                          View(await _context.StdDisbleds.ToListAsync()) :
                          Problem("Entity set 'FinalDbContext.StdDisbleds'  is null.");
        }

        // GET: Administrator/StdDisbleds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StdDisbleds == null)
            {
                return NotFound();
            }

            var stdDisbled = await _context.StdDisbleds
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stdDisbled == null)
            {
                return NotFound();
            }

            return View(stdDisbled);
        }

        // GET: Administrator/StdDisbleds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrator/StdDisbleds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AssistanceNeeded,Place,Date,Time,AcceptedBy,UserId")] StdDisbled stdDisbled)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stdDisbled);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stdDisbled);
        }

        // GET: Administrator/StdDisbleds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StdDisbleds == null)
            {
                return NotFound();
            }

            var stdDisbled = await _context.StdDisbleds.FindAsync(id);
            if (stdDisbled == null)
            {
                return NotFound();
            }
            return View(stdDisbled);
        }

        // POST: Administrator/StdDisbleds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AssistanceNeeded,Place,Date,Time,AcceptedBy,UserId")] StdDisbled stdDisbled)
        {
            if (id != stdDisbled.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stdDisbled);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StdDisbledExists(stdDisbled.Id))
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
            return View(stdDisbled);
        }

        // GET: Administrator/StdDisbleds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StdDisbleds == null)
            {
                return NotFound();
            }

            var stdDisbled = await _context.StdDisbleds
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stdDisbled == null)
            {
                return NotFound();
            }

            return View(stdDisbled);
        }

        // POST: Administrator/StdDisbleds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StdDisbleds == null)
            {
                return Problem("Entity set 'FinalDbContext.StdDisbleds'  is null.");
            }
            var stdDisbled = await _context.StdDisbleds.FindAsync(id);
            if (stdDisbled != null)
            {
                _context.StdDisbleds.Remove(stdDisbled);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StdDisbledExists(int id)
        {
          return (_context.StdDisbleds?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
