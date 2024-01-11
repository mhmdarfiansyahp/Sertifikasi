using Microsoft.AspNetCore.Mvc;

namespace Sertifikasi.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Contoh autentikasi sederhana (gantilah dengan logika autentikasi yang sesuai)
            if (username == "Admin" && password == "Admin")
            {
                // Autentikasi berhasil, arahkan ke Dashboard Index
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                // Autentikasi gagal, beri notifikasi
                TempData["ErrorMessage"] = "Invalid username or password.";
                return RedirectToAction("Index","Login") ;
            }
        }
    }
}
