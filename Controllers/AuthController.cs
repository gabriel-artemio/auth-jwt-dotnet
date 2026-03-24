using AuthJwtWebApi.DAL;
using AuthJwtWebApi.Models;
using AuthJwtWebApi.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace AuthJwtWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly IConfiguration _configuration;
        private readonly UserDAL _userDAL;

        public AuthController(TokenService tokenService, IConfiguration configuration)
        {
            _tokenService = tokenService;
            _configuration = configuration;
            _userDAL = new UserDAL();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User login)
        {
            if (string.IsNullOrWhiteSpace(login?.Email) || string.IsNullOrWhiteSpace(login?.Senha))
            {
                return BadRequest("É necessário informar usuário e senha.");
            }

            try
            {
                using (var cn = new SqliteConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    cn.Open();

                    var usuario = _userDAL.GetLogin(cn, login.Email, login.Senha);

                    if (usuario == null)
                    {
                        return Unauthorized("Usuário ou senha inválidos.");
                    }

                    var token = _tokenService.GenerateToken(usuario);

                    return Ok(new
                    {
                        token
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}