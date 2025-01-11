using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Api.Model.Comment;
using MovieApp.Domain.Entity;
using MovieApp.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using MovieApp.Api.Services.RabbitMQ;

namespace MovieApp.Api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : BaseController
    {
        private readonly MovieDbContext _context;
        private readonly IRabbitMQService _rabbitMQService;
        private const string QUEUE_NAME = "comments_queue";

        public CommentController(MovieDbContext context, IRabbitMQService rabbitMQService)
        {
            _context = context;
            _rabbitMQService = rabbitMQService;
        }

        [HttpGet("movie/{movieId:int}")]
        public async Task<IActionResult> GetMovieComments(int movieId)
        {
            var comments = await _context.Comments
                .Where(c => c.MovieId == movieId)
                .Include(c => c.User)
                .OrderBy(c => c.CreatedDate)
                .Select(c => new
                {
                    c.Id,
                    c.Text,
                    c.CreatedDate,

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
        public IActionResult SendComment([FromBody] SendCommentRequestModel req)
        {
            try
            {
                var comment = new Comment
                {
                    Text = req.Comment,
                    UserId = GetUserId(),
                    MovieId = req.Id,
                    CreatedDate = DateTime.UtcNow
                };

                _rabbitMQService.SendMessage(comment, QUEUE_NAME);
                
                return Ok(new { message = "Yorum kuyruğa eklendi" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Hata: {ex.Message}" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var userId = GetUserId();
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);

            if (comment == null)
                return NotFound();

            if (comment.UserId != userId)
                return Unauthorized();

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] UpdateCommentRequestModel model)
        {
            var userId = GetUserId();
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);

            if (comment == null)
                return NotFound();

            if (comment.UserId != userId)
                return Unauthorized();

            comment.Text = model.Comment;
            comment.ModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("test-rabbitmq")]
        public IActionResult TestRabbitMQ()
        {
            try
            {
                var testComment = new Comment
                {
                    Text = "Test mesajı - " + DateTime.Now.ToString(),
                    UserId = "test-user",
                    MovieId = 1,
                    CreatedDate = DateTime.UtcNow
                };

                Console.WriteLine("Test mesajı gönderiliyor...");
                _rabbitMQService.SendMessage(testComment, QUEUE_NAME);
                Console.WriteLine("Test mesajı gönderildi!");

                return Ok(new { 
                    message = "Test mesajı kuyruğa eklendi",
                    comment = testComment 
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Test hatası: {ex.Message}");
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }

}
