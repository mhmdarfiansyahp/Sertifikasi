using Microsoft.AspNetCore.Mvc;

namespace Sertifikasi.Controllers
{
    public class SertifikasiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
