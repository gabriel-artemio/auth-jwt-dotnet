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

                    var usuario = _userDAL.GetByEmail(cn, login.Email);

                    if (usuario == null)
                    {
                        return Unauthorized("Usuário ou senha inválidos.");
                    }

                    var hash = PasswordHelper.HashPassword("123");

                    var teste = PasswordHelper.VerifyPassword("123", hash);

                    Console.WriteLine(teste); // tem que dar TRUE

                    var senhaValida = PasswordHelper.VerifyPassword(login.Senha, usuario.Senha);

                    if (!senhaValida)
                    {
                        return Unauthorized("Usuário ou senha inválidos.");
                    }

                    var token = _tokenService.GenerateToken(usuario);

                    return Ok(new { token });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}