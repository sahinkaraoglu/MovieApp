using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Application.Services.Jwt;
using MovieApp.Domain.Entity;
using MovieApp.Infrastructure.Context;
using MovieApp.Infrastructure.MovieDb;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;

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

        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordRequestModel req)
        {
            var user = await _userManager.FindByEmailAsync(req.Email);
            
            var resToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Send token to email            

            //var apiKey = "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"; // Replace with your Brevo API key

            //Configuration.Default.ApiKey.Add("api-key", apiKey);

            var _emailApi = new TransactionalEmailsApi();

            var fromEmail = "sahinkaraoglu34@gmail.com";
            var fromName = "Your Name";
            var toEmail = "karaoglusahin@outlook.com";
            var subject = "Test Email from .NET Core";
            var htmlContent = $"<h1>{resToken}</h1>";

            var sender = new SendSmtpEmailSender(email: fromEmail, name: fromName);
            var to = new List<SendSmtpEmailTo> { new SendSmtpEmailTo(email: toEmail) };

            var email = new SendSmtpEmail
            {
                Sender = sender,
                To = to,
                Subject = subject,
                HtmlContent = htmlContent
            };

            try
            {
                var response = _emailApi.SendTransacEmail(email);
                Console.WriteLine($"Email sent successfully! Message ID: {response.MessageId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
            }


            return Ok(new
            {
                token = resToken
            });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestModel req)
        {
            var user = await _userManager.FindByEmailAsync(req.Email);

            var result = await _userManager.ResetPasswordAsync(user, req.Token, req.Password);

            if (result.Succeeded) { 
                return Ok();
            }
            
            return BadRequest();
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

    public class ForgetPasswordRequestModel
    {
        public string Email { get; set; }
    }

    public class ResetPasswordRequestModel
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
    }
}
