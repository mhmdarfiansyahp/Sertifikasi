using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sertifikasi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Sertifikasi.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(loginModel loginModel)
        {
            try
            {
                if (loginModel == null || loginModel.username == null || loginModel.password == null)
                {
                    return Json(new { code = 550, success = false, message = "Username atau Password kosong" });
                }

                var jsonResponse = await GetJwtTokenFromWebApi(loginModel);

                if (jsonResponse != null)
                {
                    var tokenResponse = JsonConvert.DeserializeObject<ResponseModel>(jsonResponse);

                    if (tokenResponse.status == 210)
                    {
                        return Json(new { success = false, message = "Nama Pengguna atau Kata Sandi salah." });
                    }
                    else if (tokenResponse.status == 220)
                    {
                        return Json(new { success = false, message = "Terjadi Kesalahan saat Login." });
                    }
                    else
                    {
                        var token = tokenResponse.data;

                        var tokenInfo = getStatusDecodeToken(token);

                        var role = tokenInfo["role"];
                        var username = tokenInfo["username"];
                        var nama = tokenInfo["name"];

                        HttpContext.Session.SetString("JwtToken", token);
                        HttpContext.Session.SetString("Name", nama);
                        HttpContext.Session.SetString("Role", role);

                        return Json(new { success = true, message = "Berhasil Login sebagai " + role + ".", role = role });
                    }
                }
                else
                {
                    return Json(new { code = 500, success = false, message = "Terjadi kesalahan saat Login" });
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Exception: {ex}");

                // Return a generic error message
                return Json(new { success = false, message = "Terjadi kesalahan saat Login" });
            }
        }
        private async Task<string> GetJwtTokenFromWebApi(loginModel loginModel)
        {
            try { 
                var endpoint = "https://localhost:44303/api/token/submit";
                
                using var httpClient = _httpClientFactory.CreateClient();

                var content = new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(endpoint, content);

                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadAsStringAsync();
                    return token;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public Dictionary<string, string> getStatusDecodeToken(string jwtToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(jwtToken);

            // Mendapatkan claim-claim dari token
            var claims = token.Claims;

            // Membuat dictionary untuk menyimpan nilai claim dengan key yang sesuai
            var claimDictionary = new Dictionary<string, string>();

            // Menambahkan nilai-nilai claim ke dalam dictionary
            claimDictionary.Add("role", claims.FirstOrDefault(c => c.Type == "role")?.Value);
            claimDictionary.Add("username", claims.FirstOrDefault(c => c.Type == "username")?.Value);
            claimDictionary.Add("name", claims.FirstOrDefault(c => c.Type == "name")?.Value);

            // Anda juga bisa menambahkan nilai claim lainnya

            return claimDictionary;
        }

        /*
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
                        TempData["ErrorMessage"] = "Nama pengguna atau Password salah";
                        return RedirectToAction("Index", "Login");
                    }

                    // Add a default return statement (this can be modified based on your requirements)
                    return RedirectToAction("Index", "Login");

                }*/
    }
}
