using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sertifikasi.Models;

namespace Sertifikasi.Controllers
{
    public class DetailSertifikasiController : Controller
    {


        public IActionResult Create()
        {
            return View();
        }

        public IActionResult index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

    }
}
