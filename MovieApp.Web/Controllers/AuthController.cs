using Microsoft.AspNetCore.Mvc;
using MovieApp.Infrastructure.Models.MovieDb.PopularTvSeries;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MovieApp.Web.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel loginReq)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync("https://localhost:7063/api/auth/login", loginReq);

                    if (!response.IsSuccessStatusCode)
                    {
                        return BadRequest(new { message = "Hatalı e-posta veya parola." });
                    }

                    var content = await response.Content.ReadAsStringAsync();
                    var loginResponse = JsonSerializer.Deserialize<LoginResponseModel>(content);

                    if (loginResponse == null || string.IsNullOrEmpty(loginResponse.Token))
                    {
                        return BadRequest(new { message = "Giriş işlemi başarısız oldu." });
                    }

                    HttpContext.Session.SetString("jwt", loginResponse.Token);
                    HttpContext.Session.SetString("name", loginResponse.Name);

                    return Ok(new { redirectUrl = "/" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var userJwt = HttpContext.Session.GetString("jwt");

            if (string.IsNullOrEmpty(userJwt))
                return Redirect("/Auth/Login");

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {userJwt}");
                var response = await client.GetAsync("https://localhost:7063/api/auth/admin");
                await response.Content.ReadAsStringAsync();
            }

            return Redirect("/");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/");
        }
    }

    public class LoginRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponseModel
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
