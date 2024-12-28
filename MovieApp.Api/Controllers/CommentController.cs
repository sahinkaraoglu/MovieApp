using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Api.Model.Comment;
using MovieApp.Domain.Entity;
using MovieApp.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace MovieApp.Api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : BaseController
    {
        private readonly MovieDbContext _context;
        public CommentController(MovieDbContext context)
        {
            _context = context;
        }

        [HttpGet("movie/{movieId:int}")]
        public async Task<IActionResult> GetMovieComments(int movieId)
        {
            var comments = await _context.Comments
                .Where(c => c.MovieId == movieId)
                .Include(c => c.User)
                .OrderByDescending(c => c.CreateDate)
                .Select(c => new
                {
                    c.Id,
                    c.Text,
                    c.CreateDate,

                    User = new
                    {
                        c.User.Id,
                        c.User.UserName,
                        c.User.Email
                    }
                })
                .ToListAsync();

            return Ok(comments);
        }

        [HttpPost("")]
        public async Task<IActionResult> SendComment([FromBody] SendCommentRequestModel req)
        {
            // kullanıcıdan gelen mesajı rabbit mq queue'ya gönder ve ok response dön


            // başka bir consumer class'ında bu mesajı db'ye yaz

            await _context.Comments.AddAsync(new Comment
            {
                Text = req.Comment,
                UserId = GetUserId(),
                MovieId = req.Id
            });

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var userId = GetUserId();
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
                return NotFound();

            if (comment.UserId != userId)
                return Unauthorized();

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] UpdateCommentRequestModel req)
        {
            var userId = GetUserId();
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
                return NotFound();

            if (comment.UserId != userId)
                return Unauthorized();

            comment.Text = req.Comment;
            await _context.SaveChangesAsync();

            return Ok();
        }
    }

}
