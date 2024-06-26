using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DAPM_QLTBYT.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "CV00000001")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
