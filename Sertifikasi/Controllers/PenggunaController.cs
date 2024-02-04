using Microsoft.AspNetCore.Mvc;

namespace Sertifikasi.Controllers
{
    public class PenggunaController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Role") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewData["JwtToken"] = HttpContext.Session.GetString("JwtToken");

            return View();
        }

        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Role") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewData["JwtToken"] = HttpContext.Session.GetString("JwtToken");

            return View();
        }

        public IActionResult Update(int iduser)
        {
            if (HttpContext.Session.GetString("Role") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewData["JwtToken"] = HttpContext.Session.GetString("JwtToken");
            ViewData["IdUser"] = iduser;

            return View();
        }


    }
}
