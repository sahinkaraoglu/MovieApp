using Microsoft.AspNetCore.Mvc;
using MovieApp.Infrastructure.Models.MovieDb.PopularTvSeries;
using MovieApp.Infrastructure.Models.MovieDb.TvSeriesDetail;
using System.Text.Json;
using MovieApp.Infrastructure.MovieDb;

namespace MovieApp.Web.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ILogger<MoviesController> _logger;
        private readonly IMovieDbApi _movieDbApi;

        public MoviesController(ILogger<MoviesController> logger, IMovieDbApi movieDbApi)
        {
            _logger = logger;
            _movieDbApi = movieDbApi;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _movieDbApi.GetPopularMoviesAsync();
            return View(movies);
        }

        [HttpGet("movies/{id:int}")]
        public async Task<IActionResult> Detail(int id)
        {
            var movie = await _movieDbApi.GetMovieDetailByIdAsync(id);
            var response = new MovieDetailResponseModel
            {
                data = movie
            };
            return View(response);
        }

        [HttpPost("movies/{id:int}/comment")]
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

        [HttpDelete("movies/{movieId:int}/comment/{commentId:int}")]
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

        [HttpPut("movies/{movieId:int}/comment/{commentId:int}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int movieId, [FromRoute] int commentId, [FromBody] UpdateCommentRequest req)
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