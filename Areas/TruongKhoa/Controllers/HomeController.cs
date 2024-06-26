using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DAPM_QLTBYT.Areas.TruongKhoa.Controllers
{
    [Area("TruongKhoa")]
    [Authorize(Roles = "CV00000004")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
