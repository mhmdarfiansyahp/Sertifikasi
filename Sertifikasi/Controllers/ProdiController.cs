using Microsoft.AspNetCore.Mvc;
using Sertifikasi.Models;
using System.Diagnostics;

namespace Sertifikasi.Controllers
{
    public class ProdiController : Controller
    {
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
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Update(int id)
        {
            ViewBag.Id = id;
            return View();

        }
    }
}
