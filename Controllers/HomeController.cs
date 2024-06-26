using DAPM_QLTBYT.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace DAPM_QLTBYT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DoAnPhanMemQltbytContext _context;


        public HomeController(ILogger<HomeController> logger, DoAnPhanMemQltbytContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Login(Nguoidung nguoidung)
        {
            var user = _context.Nguoidungs.FirstOrDefault(x => x.Email == nguoidung.Email && x.Matkhau == nguoidung.Matkhau);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.TenNd),
                    new Claim("MaNd", user.MaNd),
                    new Claim(ClaimTypes.Role, user.MaChucVu)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                if (user.TrangThai == true) {
                    if (user.MaChucVu == "CV00000001")
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                    else if (user.MaChucVu == "CV00000004")
                    {
                        HttpContext.Session.SetString("MaNd", user.MaNd);
                        HttpContext.Session.SetString("MaKhoa", user.MaKhoa);
                        return RedirectToAction("Index", "Home", new { area = "TruongKhoa" });
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Tài khoản đang tạm khóa.");
                    return View("Index", nguoidung);
                }

            }
            ModelState.AddModelError(string.Empty, "Tài khoán hoặc mật khẩu không đúng.");
            return View("Index", nguoidung);
           
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
