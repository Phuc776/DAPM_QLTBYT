using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAPM_QLTBYT.Models;
using Microsoft.AspNetCore.Authorization;

namespace DAPM_QLTBYT.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "CV00000001")]
    public class ChucvusController : Controller
    {
        private readonly DoAnPhanMemQltbytContext _context;

        public ChucvusController(DoAnPhanMemQltbytContext context)
        {
            _context = context;
        }

        // GET: Admin/Chucvus
        public async Task<IActionResult> Index()
        {
            return View(await _context.Chucvus.ToListAsync());
        }

        // GET: Admin/Chucvus/Details/CV00000001
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chucvu = await _context.Chucvus
                .FirstOrDefaultAsync(m => m.MaChucVu == id);
            if (chucvu == null)
            {
                return NotFound();
            }

            return View(chucvu);
        }

        private string GenerateNextMaChucVu()
        {
            var lastChucVu = _context.Chucvus
                .OrderByDescending(c => c.MaChucVu)
                .FirstOrDefault();

            if (lastChucVu == null)
            {
                return "CV00000001";
            }

            var lastNumber = int.Parse(lastChucVu.MaChucVu.Substring(2));
            return $"CV{(lastNumber + 1).ToString("D8")}";
        }

        // GET: Admin/Chucvus/Create
        public IActionResult Create()
        {
            var nextMaChucVu = GenerateNextMaChucVu();
            var model = new Chucvu
            {
                MaChucVu = nextMaChucVu
            };
            return View(model);
        }

        // POST: Admin/Chucvus/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaChucVu,TenChucVu")] Chucvu chucvu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chucvu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chucvu);
        }

        // GET: Admin/Chucvus/Edit/CV00000001
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chucvu = await _context.Chucvus.FindAsync(id);
            if (chucvu == null)
            {
                return NotFound();
            }
            return View(chucvu);
        }

        // POST: Admin/Chucvus/Edit/CV00000001
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaChucVu,TenChucVu")] Chucvu chucvu)
        {
            if (id != chucvu.MaChucVu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chucvu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChucvuExists(chucvu.MaChucVu))
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
            return View(chucvu);
        }

        // GET: Admin/Chucvus/Delete/CV00000001
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chucvu = await _context.Chucvus
                .FirstOrDefaultAsync(m => m.MaChucVu == id);
            if (chucvu == null)
            {
                return NotFound();
            }

            return View(chucvu);
        }

        // POST: Admin/Chucvus/Delete/CV00000001
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var chucvu = await _context.Chucvus.FindAsync(id);
            if (chucvu != null)
            {
                _context.Chucvus.Remove(chucvu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChucvuExists(string id)
        {
            return _context.Chucvus.Any(e => e.MaChucVu == id);
        }
    }
}
