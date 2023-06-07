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
using Org.BouncyCastle.Bcpg;
using HelpFinal.Models.ViewModels;

namespace HelpFinal.Areas.Users.Controllers
{
    [Area("Users")]
    public class StdDisbledsController : Controller
    {
        private readonly FinalDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public StdDisbledsController(FinalDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }


        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
              
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                
                var stdDisbleds = await _context.StdDisbleds
                    .Where(s => s.UserId == userId)
                    .ToListAsync();

                return View(stdDisbleds);
            }
            else
            {
               
                return RedirectToAction("Login", "Account");
            }
        }

      
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

      
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( StdDisbledViewModel model)
        {
            if (ModelState.IsValid)
            {

                string studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                
                model.UserId = studentId;
              
                    string ImgName = UploadFile(model);
                    StdDisbled std = new StdDisbled
                    {
                        Id = model.Id,
                        AssistanceNeeded = model.AssistanceNeeded,
                        Time = model.Time,
                        Date = model.Date,
                        Description = model.Description,
                        Place = model.Place,
                        AcceptedBy = model.AcceptedBy,
                        UserId = model.UserId,
                        Img = ImgName
                    };
                   
                    _context.StdDisbleds.Add(std);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                return View(model);
            }
         
       




        public IActionResult AcceptedPosts()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var acceptedRequests = _context.StdDisbleds.Where(r => r.AcceptedBy != null && r.UserId == userId ).ToList();
            return View(acceptedRequests);
        }


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
        public string UploadFile(StdDisbledViewModel model)
        {
            string wwwPath = _webHostEnvironment.WebRootPath;
            if (string.IsNullOrEmpty(wwwPath)) { }
            string ContentPath = _webHostEnvironment.ContentRootPath;
            if (string.IsNullOrEmpty(ContentPath)) { }
            string p = Path.Combine(wwwPath, "Images");
            if (!Directory.Exists(p))
            {
                Directory.CreateDirectory(p);
            }
            string fileName = Path.GetFileNameWithoutExtension(model.Img!.FileName);
            string newImgName = "nextwo_" + fileName + "_" + Guid.NewGuid().ToString() + Path.GetExtension(model.Img.FileName);
            using (FileStream file = new FileStream(Path.Combine(p, newImgName), FileMode.Create))
            {
                model.Img.CopyTo(file);
            }
            return "\\Images\\" + newImgName;
        }
    }
}
