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
            var movieResponse = JsonSerializer.Deserialize<TvSeriesDetailModel>(content);

            return View(movieResponse);
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
    }
}
