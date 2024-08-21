using Microsoft.AspNetCore.Mvc;

namespace MovieApp.Web.Controllers
{
    public class WatchList : Controller
    {
        public async Task<IActionResult> Index()
        {
            var userJwt = HttpContext.Session.GetString("jwt");

            if (string.IsNullOrEmpty(userJwt))
                return Redirect("/Auth/Login");

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {userJwt}");

            var response = await client.GetAsync("https://localhost:7063/api/auth/admin");
            var content = await response.Content.ReadAsStringAsync();
            client.Dispose();
            return Ok();
    }
    }
}
