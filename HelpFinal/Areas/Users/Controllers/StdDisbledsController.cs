using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HelpFinal.Data;
using HelpFinal.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HelpFinal.Areas.Users.Controllers
{
    [Area("Users")]
    public class StdDisbledsController : Controller
    {
        private readonly FinalDbContext _context;

        public StdDisbledsController(FinalDbContext context)
        {
            _context = context;
        }

        // GET: Users/StdDisbleds

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Get the authenticated user's ID
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Retrieve the stdDisbled objects for the logged-in user
                var stdDisbleds = await _context.StdDisbleds
                    .Where(s => s.UserId == userId)
                    .ToListAsync();

                return View(stdDisbleds);
            }
            else
            {
                // User is not authenticated, handle accordingly
                // For example, redirect to login page or display an error message
                return RedirectToAction("Login", "Account");
            }
        }

        //public async Task<IActionResult> Index()
        //{
       
        //    return _context.StdDisbleds != null ? 
        //                  View(await _context.StdDisbleds.ToListAsync()) :
        //                  Problem("Entity set 'FinalDbContext.StdDisbleds'  is null.");
        //}

        // GET: Users/StdDisbleds/Details/5
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

        // GET: Users/StdDisbleds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/StdDisbleds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( StdDisbled stdDisbled)
        {
            if (ModelState.IsValid)
            {
                string studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Assign the user ID to the StudentId property
                stdDisbled.UserId = studentId;
                _context.StdDisbleds.Add(stdDisbled);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stdDisbled);
        }



        public IActionResult AcceptedPosts()
        {
            var acceptedRequests = _context.StdDisbleds.Where(r => r.AcceptedBy != null).ToList();
            return View(acceptedRequests);
        }





        // GET: Users/StdDisbleds/Edit/5
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StudentID,DisabilityType,AssistanceNeeded,Place,Date,Time,PhoneNumber")] StdDisbled stdDisbled)
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

        // GET: Users/StdDisbleds/Delete/5
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

        // POST: Users/StdDisbleds/Delete/5
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
