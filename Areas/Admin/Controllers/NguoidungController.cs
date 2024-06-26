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
    public class NguoidungController : Controller
    {
        private readonly DoAnPhanMemQltbytContext _context;

        public NguoidungController(DoAnPhanMemQltbytContext context)
        {
            _context = context;
        }

        // GET: Admin/Nguoidung
        public async Task<IActionResult> Index()
        {
            var doAnPhanMemQltbytContext = _context.Nguoidungs.Include(n => n.MaChucVuNavigation).Include(n => n.MaKhoaNavigation);
            return View(await doAnPhanMemQltbytContext.ToListAsync());
        }

        // GET: Admin/Nguoidung/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nguoidung = await _context.Nguoidungs
                .Include(n => n.MaChucVuNavigation)
                .Include(n => n.MaKhoaNavigation)
                .FirstOrDefaultAsync(m => m.MaNd == id);
            if (nguoidung == null)
            {
                return NotFound();
            }

            return View(nguoidung);
        }

        private string GenerateNextMaNguoiDung()
        {
            var lastNguoiDung = _context.Nguoidungs
                .OrderByDescending(c => c.MaNd)
                .FirstOrDefault();

            if (lastNguoiDung == null)
            {
                return "ND00000001";
            }

            var lastNumber = int.Parse(lastNguoiDung.MaNd.Substring(2));
            return $"ND{(lastNumber + 1).ToString("D8")}";
        }

        // GET: Admin/Nguoidung/Create
        public IActionResult Create()
        {
            ViewData["MaChucVu"] = new SelectList(_context.Chucvus, "MaChucVu", "MaChucVu");
            ViewData["MaKhoa"] = new SelectList(_context.Khoas, "MaKhoa", "MaKhoa");
            var nextMaNguoiDung = GenerateNextMaNguoiDung();
            var model = new Nguoidung
            {
                MaNd = nextMaNguoiDung
            };
            return View(model);
        }

        // POST: Admin/Nguoidung/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaNd,TenNd,Matkhau,Email,NgaySinh,Cccd,DiaChi,TrangThai,MaChucVu,MaKhoa")] Nguoidung nguoidung)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nguoidung);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaChucVu"] = new SelectList(_context.Chucvus, "MaChucVu", "MaChucVu", nguoidung.MaChucVu);
            ViewData["MaKhoa"] = new SelectList(_context.Khoas, "MaKhoa", "MaKhoa", nguoidung.MaKhoa);
            return View(nguoidung);
        }

        // GET: Admin/Nguoidung/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nguoidung = await _context.Nguoidungs.FindAsync(id);
            if (nguoidung == null)
            {
                return NotFound();
            }
            ViewData["MaChucVu"] = new SelectList(_context.Chucvus, "MaChucVu", "MaChucVu", nguoidung.MaChucVu);
            ViewData["MaKhoa"] = new SelectList(_context.Khoas, "MaKhoa", "MaKhoa", nguoidung.MaKhoa);
            return View(nguoidung);
        }

        // POST: Admin/Nguoidung/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaNd,TenNd,Matkhau,Email,NgaySinh,Cccd,DiaChi,TrangThai,MaChucVu,MaKhoa")] Nguoidung nguoidung)
        {
            if (id != nguoidung.MaNd)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nguoidung);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NguoidungExists(nguoidung.MaNd))
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
            ViewData["MaChucVu"] = new SelectList(_context.Chucvus, "MaChucVu", "MaChucVu", nguoidung.MaChucVu);
            ViewData["MaKhoa"] = new SelectList(_context.Khoas, "MaKhoa", "MaKhoa", nguoidung.MaKhoa);
            return View(nguoidung);
        }

        // GET: Admin/Nguoidung/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nguoidung = await _context.Nguoidungs
                .Include(n => n.MaChucVuNavigation)
                .Include(n => n.MaKhoaNavigation)
                .FirstOrDefaultAsync(m => m.MaNd == id);
            if (nguoidung == null)
            {
                return NotFound();
            }

            return View(nguoidung);
        }

        // POST: Admin/Nguoidung/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var nguoidung = await _context.Nguoidungs.FindAsync(id);
            if (nguoidung != null)
            {
                _context.Nguoidungs.Remove(nguoidung);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NguoidungExists(string id)
        {
            return _context.Nguoidungs.Any(e => e.MaNd == id);
        }

        
    }
}
