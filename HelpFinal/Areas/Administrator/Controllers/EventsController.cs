using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HelpFinal.Data;
using HelpFinal.Models;
using HelpFinal.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace HelpFinal.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize]
    public class EventsController : Controller
    {
        private readonly FinalDbContext _context;
        private IWebHostEnvironment _webHostEnvironment;
        public EventsController(FinalDbContext context,IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Administrator/Events
        public async Task<IActionResult> Index()
        {
              return _context.Events.Where(x=>x.IsDeleted==false) != null ? 
                          View(await _context.Events.ToListAsync()) :
                          Problem("Entity set 'FinalDbContext.Events'  is null.");
        }

        // GET: Administrator/Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Administrator/Events/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( EventViewModel model)
        {
            if (ModelState.IsValid)
            {
                string ImgName = UploadFile(model);
                Event @event = new Event {
                 EventId = model.EventId,
                 EventTitle = model.EventTitle,
                 Time = model.Time,
                 TxtLink = model.TxtLink,
                 Date = model.Date,
                 EventDesc = model.EventDesc,
                 EventLocation = model.EventLocation,
                 UrlLink = model.UrlLink,
                 CreationDate = model.CreationDate,
                 IsDeleted = model.IsDeleted,
                 IsPublished = model.IsPublished,
                 UserId = model.UserId,
                 EventImg = ImgName
                };
                _context.Events.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Administrator/Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,EventTitle,EventDesc,EventImg,EventLocation,TxtLink,UrlLink,Date,Time,CreationDate,IsPublished,IsDeleted,UserId")] Event @event)
        {
            if (id != @event.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.EventId))
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
            return View(@event);
        }

        // GET: Administrator/Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Administrator/Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Events == null)
            {
                return Problem("Entity set 'FinalDbContext.Events'  is null.");
            }
            var @event = await _context.Events.FindAsync(id);
            if (@event != null)
            {
                _context.Events.Remove(@event);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
          return (_context.Events?.Any(e => e.EventId == id)).GetValueOrDefault();
        }

        public string UploadFile(EventViewModel model)
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
            string fileName = Path.GetFileNameWithoutExtension(model.EventImg!.FileName);
            string newImgName = "nextwo_" + fileName + "_" + Guid.NewGuid().ToString() + Path.GetExtension(model.EventImg.FileName);
            using (FileStream file = new FileStream(Path.Combine(p,newImgName),FileMode.Create))
            {
                model.EventImg.CopyTo(file);
            }
            return "\\Images\\" + newImgName;
        }
        public async Task<IActionResult> SoftDelete(int id)
        {
            var data = _context.Events.Find(id);
            if (id != data!.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    data.IsDeleted = true;
                    _context.Events.Update(data);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(data.EventId))
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
