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
            HttpClient client = new HttpClient();
            var response = await client.PostAsJsonAsync("https://localhost:7063/api/auth/login", loginReq);
            var content = await response.Content.ReadAsStringAsync();
            client.Dispose();
            var loginResponse = JsonSerializer.Deserialize<LoginResponseModel>(content);

            HttpContext.Session.SetString("jwt", loginResponse.Token);
            HttpContext.Session.SetString("name", loginResponse.Name);

            return Redirect("/");
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var userJwt = HttpContext.Session.GetString("jwt");

            if (string.IsNullOrEmpty(userJwt))
                return Redirect("/Auth/Login");

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {userJwt}");

            var response = await client.GetAsync("https://localhost:7063/api/auth/admin");
            var content = await response.Content.ReadAsStringAsync();
            client.Dispose();

            return Redirect("/");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
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
