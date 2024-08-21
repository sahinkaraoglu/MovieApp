using Microsoft.AspNetCore.Mvc;
using MovieApp.Infrastructure.MovieDb;

namespace MovieApp.Api.Controllers
{
    [Route("api/movie")]
    [ApiController]
    public class MovieController : BaseController
    {
        private readonly IMovieDbApi _movieDbApi;
        public MovieController(IMovieDbApi movieDbApi)
        {
            _movieDbApi = movieDbApi;
        }

        [HttpGet]
        public async Task<IActionResult> GetMoviesAsync()
        {
            var res = await _movieDbApi.GetMoviesAsync();
            return Ok(res);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMovieByIdAsync(int id)
        {
            var res = await _movieDbApi.GetMovieByIdAsync(id);
            return Ok(res);
        }
    }
}
