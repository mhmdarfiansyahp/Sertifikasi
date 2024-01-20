using Microsoft.AspNetCore.Mvc;
using Sertifikasi.Model;
using System.Net.Http.Headers;

namespace Sertifikasi.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string _baseaddress;

        public DashboardController(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseaddress = _configuration["ApiSettings:BaseAddress"];
        }
        public IActionResult Index()
        {
            return View();
        }
        /*
                [HttpGet]
                public async Task<IActionResult> Index()
                {

                    IEnumerable<DetailSertiModel> listdispenser;
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(_baseaddress);
                        HttpResponseMessage response = await client.GetAsync("GetAllDetail");

                        if (response.IsSuccessStatusCode)
                        {
                            listdispenser = await response.Content.ReadAsAsync<IEnumerable<DetailSertiModel>>();

                            return View(listdispenser);
                        }
                        *//*return NotFound();*//*
                        return RedirectToAction("Login", "Login");
                    }
                }*/

        public IActionResult Create()
        {
            return View();
        }
    }
}
