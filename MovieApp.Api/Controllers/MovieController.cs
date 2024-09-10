using Microsoft.AspNetCore.Mvc;
using MovieApp.Infrastructure.MovieDb;
using Newtonsoft.Json;
using StackExchange.Redis;

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
            var movieKey = "movies";
            var redis = ConnectionMultiplexer.Connect("localhost:6379");
            var db = redis.GetDatabase();

            var cacheMovies = await db.StringGetAsync(movieKey);

            //if (cacheMovies.HasValue)
            //{
                
            //    return Ok(cacheMovies.ToString());
            //}
            
            var res = await _movieDbApi.GetMoviesAsync();
            await db.StringSetAsync(movieKey, JsonConvert.SerializeObject(res));

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
