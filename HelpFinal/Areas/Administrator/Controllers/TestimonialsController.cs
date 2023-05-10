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
using HelpFinal.Models.ViewModels;

namespace HelpFinal.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize]
    public class TestimonialsController : Controller
    {
        private readonly FinalDbContext _context;
        private IWebHostEnvironment _webHostEnvironment;

        public TestimonialsController(FinalDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Administrator/Testimonials
        public async Task<IActionResult> Index()
        {
              return _context.Testimonials.Where(x=>x.IsDeleted==false) != null ? 
                          View(await _context.Testimonials.ToListAsync()) :
                          Problem("Entity set 'FinalDbContext.Testimonials'  is null.");
        }

        // GET: Administrator/Testimonials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Testimonials == null)
            {
                return NotFound();
            }

            var testimonial = await _context.Testimonials
                .FirstOrDefaultAsync(m => m.TestimonialId == id);
            if (testimonial == null)
            {
                return NotFound();
            }

            return View(testimonial);
        }

        // GET: Administrator/Testimonials/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( TestimonialViewModel testimonial)
        {
            if (ModelState.IsValid)
            {
                string ImgName = UploadFile(testimonial);
                Testimonial test = new Testimonial {
                 TestimonialId = testimonial.TestimonialId,
                 Collage = testimonial.Collage,
                 TestimonialDesc = testimonial.TestimonialDesc,
                 TestimonialName = testimonial.TestimonialName,
                 CreationDate = testimonial.CreationDate,
                 IsDeleted = testimonial.IsDeleted,
                 IsPublished = testimonial.IsPublished,
                 UserId= testimonial.UserId,
                 Image = ImgName
                };
                _context.Testimonials.Add(test);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(testimonial);
        }

        // GET: Administrator/Testimonials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Testimonials == null)
            {
                return NotFound();
            }

            var testimonial = await _context.Testimonials.FindAsync(id);
            if (testimonial == null)
            {
                return NotFound();
            }
            return View(testimonial);
        }

        // POST: Administrator/Testimonials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TestimonialId,TestimonialName,Collage,TestimonialDesc,Image,CreationDate,IsPublished,IsDeleted,UserId")] Testimonial testimonial)
        {
            if (id != testimonial.TestimonialId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testimonial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestimonialExists(testimonial.TestimonialId))
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
            return View(testimonial);
        }

        // GET: Administrator/Testimonials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Testimonials == null)
            {
                return NotFound();
            }

            var testimonial = await _context.Testimonials
                .FirstOrDefaultAsync(m => m.TestimonialId == id);
            if (testimonial == null)
            {
                return NotFound();
            }

            return View(testimonial);
        }

        // POST: Administrator/Testimonials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Testimonials == null)
            {
                return Problem("Entity set 'FinalDbContext.Testimonials'  is null.");
            }
            var testimonial = await _context.Testimonials.FindAsync(id);
            if (testimonial != null)
            {
                _context.Testimonials.Remove(testimonial);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestimonialExists(int id)
        {
          return (_context.Testimonials?.Any(e => e.TestimonialId == id)).GetValueOrDefault();
        }

        public string UploadFile(TestimonialViewModel model)
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
            string fileName = Path.GetFileNameWithoutExtension(model.Image!.FileName);
            string newImgName = "nextwo_" + fileName + "_" + Guid.NewGuid().ToString() + Path.GetExtension(model.Image.FileName);
            using (FileStream file = new FileStream(Path.Combine(p, newImgName), FileMode.Create))
            {
                model.Image.CopyTo(file);
            }
            return "\\Images\\" + newImgName;
        }
        public async Task<IActionResult> SoftDelete(int id)
        {
            var data = _context.Testimonials.Find(id);
            if (id != data!.TestimonialId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    data.IsDeleted = true;
                    _context.Testimonials.Update(data);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestimonialExists(data.TestimonialId))
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
    }
}
