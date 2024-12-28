using Microsoft.AspNetCore.Mvc;
using MovieApp.Infrastructure.Models.MovieDb.PopularTvSeries;
using MovieApp.Infrastructure.Models.MovieDb.TvSeriesDetail;
using MovieApp.Web.Models;
using System.Diagnostics;
using System.Text.Json;

namespace MovieApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://localhost:7063/api/movie");
            var content = await response.Content.ReadAsStringAsync();
            client.Dispose();
            var movieResponse = JsonSerializer.Deserialize<PopularTvSeriesModel>(content);

            return View(movieResponse);
        }

        [HttpGet("movie/{id:int}")]
        public async Task<IActionResult> Detail(int id)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync($"https://localhost:7063/api/movie/{id}");
            var content = await response.Content.ReadAsStringAsync();
            client.Dispose();
            var movieResponse = JsonSerializer.Deserialize<MovieDetailResponseModel>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View(movieResponse);
        }

        [HttpPost("movie/{id:int}/comment")]
        public async Task<IActionResult> SendComment([FromRoute] int id, [FromBody] SendCommentRequest req)
        {
            var userJwt = HttpContext.Session.GetString("jwt");

            if (string.IsNullOrEmpty(userJwt))
                return Redirect("/Auth/Login");

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {userJwt}");

            var response = await client.PostAsJsonAsync("https://localhost:7063/api/comment", new
            {
                id = id,
                comment = req.Comment
            });
            var content = await response.Content.ReadAsStringAsync();
            client.Dispose();

            return Ok();
        }

        [HttpDelete("movie/{movieId:int}/comment/{commentId:int}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int movieId, [FromRoute] int commentId)
        {
            var userJwt = HttpContext.Session.GetString("jwt");

            if (string.IsNullOrEmpty(userJwt))
                return Unauthorized();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {userJwt}");

            var response = await client.DeleteAsync($"https://localhost:7063/api/comment/{commentId}");
            
            if (!response.IsSuccessStatusCode)
                return BadRequest();

            client.Dispose();
            return Ok();
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

        public class SendCommentRequest
        {
            public string Comment { get; set; }
        }
    }
}
