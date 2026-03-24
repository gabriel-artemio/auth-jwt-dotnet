using AuthJwtWebApi.Models;
using AuthJwtWebApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace AuthJwtWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;

        public AuthController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User login)
        {
            if (login.Email == "admin@admin.com" && login.Password == "123")
            {
                var user = new User
                {
                    Email = login.Email,
                    Role = "Admin"
                };

                var token = _tokenService.GenerateToken(user);

                return Ok(new
                {
                    token
                });
            }

            return Unauthorized();
        }
    }
}
