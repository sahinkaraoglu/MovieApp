using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.Infrastructure.Context;
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
        private readonly MovieDbContext _context;
        public MovieController(IMovieDbApi movieDbApi, MovieDbContext context)
        {
            _movieDbApi = movieDbApi;
            _context = context;
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

            var comments = await _context
                .Comments
                .Include(e => e.User)
                .Where(e => e.MovieId == id)
                .ToListAsync();

            return Ok(new
            {
                data = res,
                comments = comments
            });
        }
    }
}
