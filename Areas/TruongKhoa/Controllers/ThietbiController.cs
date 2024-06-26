using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAPM_QLTBYT.Models;
using Microsoft.AspNetCore.Authorization;

namespace DAPM_QLTBYT.Areas.TruongKhoa.Controllers
{
    [Area("TruongKhoa")]
    [Authorize(Roles = "CV00000004")]
    public class ThietbiController : Controller
    {
        private readonly DoAnPhanMemQltbytContext _context;

        public ThietbiController(DoAnPhanMemQltbytContext context)
        {
            _context = context;
        }

        // GET: TruongKhoa/Thietbi
        public async Task<IActionResult> Index()
        {
            var doAnPhanMemQltbytContext = _context.Thietbis.Include(t => t.MaDmNavigation);
            return View(await doAnPhanMemQltbytContext.ToListAsync());
        }
        // GET: TruongKhoa/Thietbi/SelectThietBi?mathietbi=5
        public async Task<IActionResult> SelectThietBi()
        {
            var thietbi = _context.Thietbis
                .Include(t => t.MaDmNavigation);
            if (thietbi == null)
            {
                return NotFound();
            }
            

            return View(await thietbi.ToListAsync());
        }
        // POST: TruongKhoa/Thietbi/SelectThietBi?mathietbi=5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SelectThietBi(string id)
        {
            var maPhieu = HttpContext.Session.GetString("truyenMaPhieu");
            string maPhieustring = maPhieu.ToString();
            HttpContext.Session.SetString("SelectedIdThietBi", id);
            HttpContext.Session.SetString("truyenlaiMaPhieu", maPhieustring);
            return RedirectToAction("AddChiTiet", "Phieuyeucaunhap");
        }
        // GET: TruongKhoa/Thietbi/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thietbi = await _context.Thietbis
                .Include(t => t.MaDmNavigation)
                .FirstOrDefaultAsync(m => m.MaTb == id);
            if (thietbi == null)
            {
                return NotFound();
            }

            return View(thietbi);
        }

        // GET: TruongKhoa/Thietbi/Create
        public IActionResult Create()
        {
            ViewData["MaDm"] = new SelectList(_context.Danhmucthietbis, "MaDm", "MaDm");
            return View();
        }

        // POST: TruongKhoa/Thietbi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaTb,TenTb,SoLuong,Xuatxu,HinhAnh,NhaCungCap,MaDm")] Thietbi thietbi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(thietbi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaDm"] = new SelectList(_context.Danhmucthietbis, "MaDm", "MaDm", thietbi.MaDm);
            return View(thietbi);
        }

        // GET: TruongKhoa/Thietbi/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thietbi = await _context.Thietbis.FindAsync(id);
            if (thietbi == null)
            {
                return NotFound();
            }
            ViewData["MaDm"] = new SelectList(_context.Danhmucthietbis, "MaDm", "MaDm", thietbi.MaDm);
            return View(thietbi);
        }

        // POST: TruongKhoa/Thietbi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaTb,TenTb,SoLuong,Xuatxu,HinhAnh,NhaCungCap,MaDm")] Thietbi thietbi)
        {
            if (id != thietbi.MaTb)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thietbi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThietbiExists(thietbi.MaTb))
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
            ViewData["MaDm"] = new SelectList(_context.Danhmucthietbis, "MaDm", "MaDm", thietbi.MaDm);
            return View(thietbi);
        }

        // GET: TruongKhoa/Thietbi/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thietbi = await _context.Thietbis
                .Include(t => t.MaDmNavigation)
                .FirstOrDefaultAsync(m => m.MaTb == id);
            if (thietbi == null)
            {
                return NotFound();
            }

            return View(thietbi);
        }

        // POST: TruongKhoa/Thietbi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var thietbi = await _context.Thietbis.FindAsync(id);
            if (thietbi != null)
            {
                _context.Thietbis.Remove(thietbi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThietbiExists(string id)
        {
            return _context.Thietbis.Any(e => e.MaTb == id);
        }
    }
}
