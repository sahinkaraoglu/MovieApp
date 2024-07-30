using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.Infrastructure.Context;

namespace MovieApp.Api.Controllers
{
    [Route("api/favorite")]
    [ApiController]
    public class FavoriteController : BaseController
    {
        private readonly MovieDbContext _context;
        public FavoriteController(MovieDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserFavorites()
        {
            var userId = GetUserId();

            if (userId == null)
                return Unauthorized();

            var favorites = await _context.Favorites.Where(e => e.UserId == userId).ToListAsync();

            return Ok(favorites);
        }
    }
}
