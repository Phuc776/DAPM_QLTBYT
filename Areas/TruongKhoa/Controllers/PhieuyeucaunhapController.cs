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
    public class PhieuyeucaunhapController : Controller
    {
        private string maPhieu;
        private readonly DoAnPhanMemQltbytContext _context;

        public PhieuyeucaunhapController(DoAnPhanMemQltbytContext context)
        {
            _context = context;
        }

        // GET: TruongKhoa/Phieuyeucaunhap
        public async Task<IActionResult> Index()
        {

            var doAnPhanMemQltbytContext = _context.Phieuyeucaunhaps
                .Include(p => p.MaKhoaNavigation)
                .Include(p => p.MaNguoiDuyetNavigation)
                .Include(p => p.MaNguoiTaoNavigation)
                .Include(p => p.MaNguoiXacNhanNavigation);
            return View(await doAnPhanMemQltbytContext.ToListAsync());
        }

        // GET: TruongKhoa/Phieuyeucaunhap/EditDetails/5
        public async Task<IActionResult> EditDetails(string? id)
        {
            if (TempData["errMessage"] != null)
            {
                ViewBag.errMessage = TempData["errMessage"];
            }
            if (id == null)
            {
                return NotFound();
            }

            var phieuyeucaunhap = await _context.Phieuyeucaunhaps
                .Include(p => p.MaKhoaNavigation)
                .Include(p => p.MaNguoiDuyetNavigation)
                .Include(p => p.Chitietphieuyeucaunhaps)
                    .ThenInclude(p => p.MaTbNavigation)
                .FirstOrDefaultAsync(m => m.MaPhieu == id);
            if (phieuyeucaunhap == null)
            {
                return NotFound();
            }
            TempData["id"] = id;
            return View(phieuyeucaunhap);
        }
        private string GeneratenewMaPhieu()
        {
            var phieu = _context.Phieuyeucaunhaps.OrderByDescending(p => p.MaPhieu).FirstOrDefault();
            if (phieu == null)
            {
                return "PN00000001";
            }
            else
            {
                string maPhieu = phieu.MaPhieu;
                int soPhieu = int.Parse(maPhieu.Substring(2)) + 1;
                return "PN" + soPhieu.ToString("D8");
            }
        }
        // GET: TruongKhoa/Phieuyeucaunhap/Create
        public IActionResult Create()
        {
            var getMaKhoaFromSession = HttpContext.Session.GetString("MaKhoa");
            var getMaNdFromSession = HttpContext.Session.GetString("MaNd");
            var newPhieu = new Phieuyeucaunhap
            {
                MaPhieu = GeneratenewMaPhieu(),
                MaKhoa = getMaKhoaFromSession,
                MaNguoiTao = getMaNdFromSession,
                NgayTao = DateOnly.FromDateTime(DateTime.Now),
                TrangThai = "Chờ duyệt"
            };

            _context.Phieuyeucaunhaps.Add(newPhieu);
            _context.SaveChanges();

            return RedirectToAction("EditDetails", new { id = newPhieu.MaPhieu });
        }
        // GET: TruongKhoa/Phieuyeucaunhap/XemCTPN/5
        public async Task<IActionResult> XemCTPN(string? id)
        {
            if (TempData["errMessage"] != null)
            {
                ViewBag.errMessage = TempData["errMessage"];
            }
            if (id == null)
            {
                return NotFound();
            }

            var doAnPhanMemQltbytContext = await _context.Phieuyeucaunhaps
                .Include(p => p.Chitietphieuyeucaunhaps)
                    .ThenInclude(p => p.MaTbNavigation)
                .FirstOrDefaultAsync(m => m.MaPhieu == id);
            if (doAnPhanMemQltbytContext == null)
            {
                return NotFound();
            }
            TempData["id"] = id;
            return View(doAnPhanMemQltbytContext);
        }

        // GET: TruongKhoa/Phieuyeucaunhap/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phieuyeucaunhap = await _context.Phieuyeucaunhaps
                .Include(p => p.MaKhoaNavigation)
                .Include(p => p.MaNguoiDuyetNavigation)
                .Include(p => p.MaNguoiTaoNavigation)
                .Include(p => p.MaNguoiXacNhanNavigation)
                .Include(p => p.Chitietphieuyeucaunhaps)
                .FirstOrDefaultAsync(m => m.MaPhieu == id);
            if (phieuyeucaunhap == null)
            {
                return NotFound();
            }

            if (phieuyeucaunhap.TrangThai != "Chờ duyệt")
            {
                return RedirectToAction(nameof(Index));
            }

            return View(phieuyeucaunhap);
        }

        // POST: TruongKhoa/Phieuyeucaunhap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var phieuyeucaunhap = await _context.Phieuyeucaunhaps
                .Include(p => p.Chitietphieuyeucaunhaps) // Include related details
                .FirstOrDefaultAsync(p => p.MaPhieu == id);

            if (phieuyeucaunhap == null)
            {
                return NotFound();
            }

            if (phieuyeucaunhap.TrangThai != "Chờ duyệt")
            {
                TempData["errMessage"] = "Không thể xóa sau duyệt";
                
                return RedirectToAction(nameof(Index));
            }
            ViewBag.errMessage = null;
            _context.Chitietphieuyeucaunhaps.RemoveRange(phieuyeucaunhap.Chitietphieuyeucaunhaps);

            _context.Phieuyeucaunhaps.Remove(phieuyeucaunhap);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        private bool PhieuyeucaunhapExists(string id)
        {
            return _context.Phieuyeucaunhaps.Any(e => e.MaPhieu == id);
        }
        public IActionResult AddChiTiet(string id)
        {
            if (id != null)
            {
                maPhieu = id;
                HttpContext.Session.SetString("truyenMaPhieu", maPhieu);
            }

            var mp = HttpContext.Session.GetString("truyenlaiMaPhieu");
            var maThietBi = HttpContext.Session.GetString("SelectedIdThietBi");
            ViewBag.SelectedIdThietBi = maThietBi;
            var tenThietBi = (from tb in _context.Thietbis
                              where tb.MaTb == maThietBi
                              select tb.TenTb).FirstOrDefault();
            if (tenThietBi != null)
            {
                ViewBag.SelectedTenTB = tenThietBi;
            }
            else
            {
                ViewBag.SelectedTenTB = null;
            }

            if (mp == null)
            {
                ViewBag.MaPhieu = id;
            }
            else
            {
                ViewBag.MaPhieu = mp;
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddChiTiet(Chitietphieuyeucaunhap chitietphieuyeucaunhap)
        {
            var ctpn = _context.Chitietphieuyeucaunhaps
                .Where(ct => ct.MaPhieu == chitietphieuyeucaunhap.MaPhieu && ct.MaTb == chitietphieuyeucaunhap.MaTb)
                .FirstOrDefault();
            if (ctpn != null)
            {
                TempData["errMessage"] = "Đã tồn tại thiết bị này trong phiếu";
                return RedirectToAction("EditDetails", new { id = chitietphieuyeucaunhap });
            }
            else
            {
                if (chitietphieuyeucaunhap.SoLuongYeuCau > 0 && chitietphieuyeucaunhap.MaPhieu != null)
                {
                    TempData["errMessage"] = null;
                    _context.Add(chitietphieuyeucaunhap);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("EditDetails", new { id = chitietphieuyeucaunhap.MaPhieu });
                }
                else
                {
                    return View(chitietphieuyeucaunhap);
                }
            }
        }
        // GET: TruongKhoa/Phieuyeucaunhap/EditChiTiet/5
        public async Task<IActionResult> EditChiTiet(string id, string maTb)
        {
            if (id == null || maTb == null)
            {
                return NotFound();
            }

            var chitietphieuyeucaunhap = await _context.Chitietphieuyeucaunhaps
                .Include(c => c.MaTbNavigation)
                .Include(c => c.MaPhieuNavigation)
                .FirstOrDefaultAsync(m => m.MaPhieu == id && m.MaTb == maTb);
            if (chitietphieuyeucaunhap == null)
            {
                return NotFound();
            }

            return View(chitietphieuyeucaunhap);
        }
        // POST: TruongKhoa/Phieuyeucaunhap/EditChiTiet/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditChiTiet(Chitietphieuyeucaunhap chitietphieuyeucaunhap)
        {
            if (chitietphieuyeucaunhap != null)
            {
                Chitietphieuyeucaunhap ctpn = _context.Chitietphieuyeucaunhaps.Find(chitietphieuyeucaunhap.MaPhieu, chitietphieuyeucaunhap.MaTb);
                ctpn.LyDo = chitietphieuyeucaunhap.LyDo;
                ctpn.SoLuongYeuCau = chitietphieuyeucaunhap.SoLuongYeuCau;
                _context.SaveChanges();
                return RedirectToAction("EditDetails", new { id = chitietphieuyeucaunhap.MaPhieu });
            }
            else
            {
                return View(chitietphieuyeucaunhap);
            }

        }

        // GET: TruongKhoa/Phieuyeucaunhap/DeleteChiTiet/5
        public async Task<IActionResult> DeleteChiTiet(string id, string maTb)
        {
            if (id == null || maTb == null)
            {
                return NotFound();
            }

            var chitietphieuyeucaunhap = await _context.Chitietphieuyeucaunhaps
                .Include(c => c.MaTbNavigation)
                .Include(c => c.MaPhieuNavigation)
                .FirstOrDefaultAsync(m => m.MaPhieu == id && m.MaTb == maTb);
            if (chitietphieuyeucaunhap == null)
            {
                return NotFound();
            }

            return View(chitietphieuyeucaunhap);
        }
        // POST: TruongKhoa/Phieuyeucaunhap/DeleteChiTiet/5
        [HttpPost, ActionName("DeleteChiTiet")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteChiTietConfirmed(string id, string maTb)
        {
            var chitietphieuyeucaunhap = await _context.Chitietphieuyeucaunhaps
                .Include(c => c.MaTbNavigation)
                .Include(c => c.MaPhieuNavigation)
                .FirstOrDefaultAsync(m => m.MaPhieu == id && m.MaTb == maTb);
            if (chitietphieuyeucaunhap == null)
            {
                return NotFound();
            }
            _context.Chitietphieuyeucaunhaps.Remove(chitietphieuyeucaunhap);
            await _context.SaveChangesAsync();
            return RedirectToAction("EditDetails", new { id = id });
        }
    }
}
