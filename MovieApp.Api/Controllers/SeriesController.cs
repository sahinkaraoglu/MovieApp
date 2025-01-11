using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.Infrastructure.Context;
using MovieApp.Infrastructure.MovieDb;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace MovieApp.Api.Controllers
{
    [Route("api/series")]
    [ApiController]
    public class SeriesController : BaseController
    {
        private readonly IMovieDbApi _movieDbApi;
        private readonly MovieDbContext _context;
        public SeriesController(IMovieDbApi movieDbApi, MovieDbContext context)
        {
            _movieDbApi = movieDbApi;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetSeriesAsync()
        {
            var seriesKey = "series";
            var redis = ConnectionMultiplexer.Connect("localhost:6379");
            var db = redis.GetDatabase();

            var cacheSeries = await db.StringGetAsync(seriesKey);

            //if (cacheSeries.HasValue)
            //{
            //    return Ok(cacheSeries.ToString());
            //}
            
            var res = await _movieDbApi.GetTvSeriesAsync();
            await db.StringSetAsync(seriesKey, JsonConvert.SerializeObject(res));

            return Ok(res);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSeriesByIdAsync(int id)
        {
            var res = await _movieDbApi.GetTvSeriesDetailByIdAsync(id);

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