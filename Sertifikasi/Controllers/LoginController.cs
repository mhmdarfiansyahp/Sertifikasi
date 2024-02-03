using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sertifikasi.Models;

namespace Sertifikasi.Controllers
{
    public class LoginController : Controller
    {
        private readonly user userrepositori;

        public LoginController(IConfiguration configuration)
        {
            userrepositori = new user(configuration);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {

            userModel userModel = userrepositori.getDataByUsername_Password(username, password);

            if (userModel != null)
            {
                // Other existing code...

                // Check the user's role and redirect accordingly
                if (userModel.role == "Super Admin")
                {
                    string serializedModelFromDb = JsonConvert.SerializeObject(userModel);
                    HttpContext.Session.SetString("Identity", serializedModelFromDb);
                    HttpContext.Session.SetString("Role", userModel.role);

                    return RedirectToAction("Index", "Dashboard");
                }
                else if (userModel.role == "Admin")
                {
                    return RedirectToAction("Index", "Dashboard");
                }
               
            }
            else
            {
                // Login failed, show error message
                TempData["ErrorMessage"] = "Username or password is incorrect.";
                return RedirectToAction("Index", "Login");
            }

            // Add a default return statement (this can be modified based on your requirements)
            return RedirectToAction("Index", "Login");

        }
    }
}
