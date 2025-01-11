using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.Infrastructure.Context;

namespace MovieApp.Api.Controllers
{
    [Route("api/list")]
    [ApiController]
    public class ListController : BaseController
    {
        private readonly MovieDbContext _context;
        public ListController(MovieDbContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetUserLists()
        {
            var userId = GetUserId();

            if (userId == null)
                return Unauthorized();

            var userLists = await _context
                .UserLists
                .Where(e => e.UserId == userId)
                .ToListAsync();

            return Ok(userLists);
        }

        [HttpGet("{userListId}/movie")]
        public async Task<IActionResult> GetListMovies(int userListId)
        {
            var userId = GetUserId();

            if (userId == null)
                return Unauthorized();

            var userList = await _context
                .UserLists
                .Include(e => e.ListMovies)
                .FirstOrDefaultAsync(e => e.UserId == userId && e.Id == userListId);

            return Ok(userList);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddList([FromBody] AddListRequestModel req)
        {
            var userId = GetUserId();

            if (userId == null)
                return Unauthorized();

            // FirstOrDefault ile kullanıcının aynı isimde listesi olup olmadığı kontrol edilmeli.
            // Aynı isim ile liste varsa yeni liste oluşturulmamalı!

            await _context.UserLists.AddAsync(new Domain.Entity.UserList
            {
                CreatedDate = DateTime.Now,
                Title = req.Title,
                UserId = userId,
            });

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{userListId}")]
        public async Task<IActionResult> UpdateList(int userListId, [FromBody] UpdateListRequestModel req)
        {
            var userId = GetUserId();

            if (userId == null)
                return Unauthorized();

            var userList = await _context
                .UserLists
                .FirstOrDefaultAsync(e => e.Id == userListId && e.UserId == userId);

            if (userList is null)
                return NotFound();

            userList.Title = req.Title;
            userList.ModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{userListId}")]
        public async Task<IActionResult> DeleteList(int userListId)
        {
            var userId = GetUserId();

            if (userId == null)
                return Unauthorized();

            var userList = await _context
                .UserLists
                .FirstOrDefaultAsync(e => e.Id == userListId && e.UserId == userId);

            if (userList is null)
                return NotFound();

            _context.UserLists.Remove(userList);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("{userListId}/movie")]
        public async Task<IActionResult> AddMovieToList(int userListId, [FromBody] AddMovieToListRequestModel req)
        {
            var userId = GetUserId();

            if (userId == null)
                return Unauthorized();

            var userList = await _context
                .UserLists
                .FirstOrDefaultAsync(e => e.Id == userListId && e.UserId == userId);

            if (userList is null)
                return NotFound();

            // FirstOrDefault ile kullanıcının aynı listede aynı id'li filmi olup olmadığı kontrol edilmeli.
            // Aynı id ile film varsa yeni satır oluşturulmamalı!

            await _context.ListMovie.AddAsync(new Domain.Entity.ListMovie
            {
                CreatedDate = DateTime.Now,
                MovieId = req.MovieId,
                UserListId = userListId
            });

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{userListId}/movie/{movieId}")]
        public async Task<IActionResult> RemoveMovieFromList(int userListId, int movieId)
        {
            var userId = GetUserId();
            
            if (userId == null)
                return Unauthorized();

            var listMovie = await _context
                .ListMovie
                .FirstOrDefaultAsync(e => e.UserListId == userListId && e.MovieId == movieId);

            if (listMovie is null)
                return NotFound();

            _context.ListMovie.Remove(listMovie);
            await _context.SaveChangesAsync();

            return Ok();
        }

    }

    public class AddListRequestModel
    {
        public string Title { get; set; }
    }

    public class UpdateListRequestModel
    {
        public string Title { get; set; }

    }

    public class AddMovieToListRequestModel
    {
        public int MovieId { get; set; }
    }

}
