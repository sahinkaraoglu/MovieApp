using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Application.Services.Jwt;
using MovieApp.Infrastructure.Context;
using MovieApp.Infrastructure.MovieDb;

namespace MovieApp.Api.Controllers
{


    [Route("api/auth")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtService _jwtService;
        private readonly IMovieDbApi _movieDbApi;
        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IJwtService jwtService, IMovieDbApi movieDbApi)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _movieDbApi = movieDbApi;
        }

        [HttpGet("demo")]
        public async Task<IActionResult> GetMoviesAsync()
        {
            var res = await _movieDbApi.GetMoviesAsync();
            return Ok(res);
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestModel req)
        {

            var user = new ApplicationUser { UserName = req.Email, Email = req.Email };
            var result = await _userManager.CreateAsync(user, req.Password);

            if (result.Succeeded)
            {
                return Created();
            } else
            {
                return BadRequest();
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel req)
        {
            var result = await _signInManager.PasswordSignInAsync(req.Email, req.Password, isPersistent: false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(req.Email);
                var token = _jwtService.GenerateJwtToken(user);
                return Ok(new
                {
                    token = token
                });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("admin")]
        [Authorize]
        public async Task<IActionResult> Admin()
        {
            var userId = GetUserId();
            return Ok(new
            {
                userId = userId
            });
        }
    }

    public class RegisterRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
