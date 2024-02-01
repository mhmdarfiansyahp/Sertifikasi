using Microsoft.AspNetCore.Mvc;

namespace Sertifikasi.Controllers
{
    public class PenggunaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
