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
using HelpFinal.Models.ViewModels;

namespace HelpFinal.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize]
    public class SlidersController : Controller
    {
        private readonly FinalDbContext _context;
        private IWebHostEnvironment _webHostEnvironment;
        public SlidersController(FinalDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Administrator/Sliders
        public async Task<IActionResult> Index()
        {
              return _context.Sliders.Where(x=>x.IsDeleted==false) != null ? 
                          View(await _context.Sliders.ToListAsync()) :
                          Problem("Entity set 'FinalDbContext.Sliders'  is null.");
        }

        // GET: Administrator/Sliders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sliders == null)
            {
                return NotFound();
            }

            var slider = await _context.Sliders
                .FirstOrDefaultAsync(m => m.SliderId == id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        // GET: Administrator/Sliders/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( SliderViewModel slider)
        {
            if (ModelState.IsValid)
            {
                string ImgName =  UploadFile(slider);
                Slider s = new Slider { 
                  SliderId= slider.SliderId,
                  SliderTitle=slider.SliderTitle,
                  SliderSubTitle=slider.SliderSubTitle,
                  TxtLink=slider.TxtLink,
                  UrlLink=slider.UrlLink,
                  CreationDate=slider.CreationDate,
                  IsDeleted=slider.IsDeleted,
                  IsPublished=slider.IsPublished,
                  UserId=slider.UserId,
                  SliderImg=ImgName
                
                };
                _context.Sliders.Add(s);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(slider);
        }

        // GET: Administrator/Sliders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sliders == null)
            {
                return NotFound();
            }

            var slider = await _context.Sliders.FindAsync(id);
            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
        }

        // POST: Administrator/Sliders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SliderId,SliderTitle,SliderSubTitle,TxtLink,UrlLink,SliderImg,CreationDate,IsPublished,IsDeleted,UserId")] Slider slider)
        {
            if (id != slider.SliderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(slider);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SliderExists(slider.SliderId))
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
            return View(slider);
        }

        // GET: Administrator/Sliders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sliders == null)
            {
                return NotFound();
            }

            var slider = await _context.Sliders
                .FirstOrDefaultAsync(m => m.SliderId == id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        // POST: Administrator/Sliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sliders == null)
            {
                return Problem("Entity set 'FinalDbContext.Sliders'  is null.");
            }
            var slider = await _context.Sliders.FindAsync(id);
            if (slider != null)
            {
                _context.Sliders.Remove(slider);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SliderExists(int id)
        {
          return (_context.Sliders?.Any(e => e.SliderId == id)).GetValueOrDefault();
        }

        public string UploadFile(SliderViewModel model)
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
            string fileName = Path.GetFileNameWithoutExtension(model.SliderImg!.FileName);
            string newImgName = "nextwo_" + fileName + "_" + Guid.NewGuid().ToString() + Path.GetExtension(model.SliderImg.FileName);
            using (FileStream file = new FileStream(Path.Combine(p, newImgName), FileMode.Create))
            {
                model.SliderImg.CopyTo(file);
            }
            return "\\Images\\" + newImgName;
        }
        public async Task<IActionResult> SoftDelete(int id)
        {
            var data = _context.Sliders.Find(id);
            if (id != data!.SliderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    data.IsDeleted = true;
                    _context.Sliders.Update(data);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SliderExists(data.SliderId))
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
