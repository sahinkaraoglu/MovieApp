using Microsoft.AspNetCore.Mvc;
using MovieApp.Infrastructure.Models.MovieDb.PopularTvSeries;
using MovieApp.Infrastructure.Models.MovieDb.TvSeriesDetail;
using System.Text.Json;

namespace MovieApp.Web.Controllers
{
    public class SeriesController : Controller
    {
        private readonly ILogger<SeriesController> _logger;

        public SeriesController(ILogger<SeriesController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://localhost:7063/api/series");
            var content = await response.Content.ReadAsStringAsync();
            client.Dispose();
            var seriesResponse = JsonSerializer.Deserialize<PopularTvSeriesModel>(content);

            return View(seriesResponse);
        }

        [HttpGet("series/{id:int}")]
        public async Task<IActionResult> Detail(int id)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync($"https://localhost:7063/api/series/{id}");
            var content = await response.Content.ReadAsStringAsync();
            client.Dispose();
            var movieResponse = JsonSerializer.Deserialize<MovieDetailResponseModel>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View(movieResponse);
        }

        [HttpPost("series/{id:int}/comment")]
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

        [HttpDelete("series/{seriesId:int}/comment/{commentId:int}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int seriesId, [FromRoute] int commentId)
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

        [HttpPut("series/{seriesId:int}/comment/{commentId:int}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int seriesId, [FromRoute] int commentId, [FromBody] UpdateCommentRequest req)
        {
            var userJwt = HttpContext.Session.GetString("jwt");

            if (string.IsNullOrEmpty(userJwt))
                return Unauthorized();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {userJwt}");

            var response = await client.PutAsJsonAsync($"https://localhost:7063/api/comment/{commentId}", new
            {
                comment = req.Comment
            });

            if (!response.IsSuccessStatusCode)
                return BadRequest();

            client.Dispose();
            return Ok();
        }

        public class SendCommentRequest
        {
            public string Comment { get; set; }
        }

        public class UpdateCommentRequest
        {
            public string Comment { get; set; }
        }
    }
} 