﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Api.Model.Comment;
using MovieApp.Domain.Entity;
using MovieApp.Infrastructure.Context;

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
    }

}
