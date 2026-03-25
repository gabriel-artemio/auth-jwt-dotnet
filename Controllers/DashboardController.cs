using AuthJwtWebApi.DAL;
using AuthJwtWebApi.Models;
using AuthJwtWebApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace AuthJwtWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserDAL _userDAL;

        public DashboardController(IConfiguration configuration)
        {
            _configuration = configuration;
            _userDAL = new UserDAL();
        }

        [HttpGet("admin")]
        [Authorize(Roles = "Admin")] 
        public IActionResult Admin() 
        {
            return Ok("Área de admin!");
        }

        [HttpGet("user")]
        [Authorize(Roles = "User")]
        public new IActionResult User()
        {
            return Ok("Área de usuário!");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] User dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nome) ||
                string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.Senha))
            {
                return BadRequest("Dados inválidos.");
            }

            try
            {
                using (var cn = new SqliteConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    cn.Open();

                    var existente = _userDAL.GetByEmail(cn, dto.Email);
                    if (existente != null)
                    {
                        return BadRequest("Usuário já existe.");
                    }

                    var senhaHash = PasswordHelper.HashPassword(dto.Senha);

                    var user = new User
                    {
                        Nome = dto.Nome,
                        Email = dto.Email,
                        Senha = senhaHash,
                        Role = string.IsNullOrWhiteSpace(dto.Role) ? "User" : dto.Role
                    };

                    _userDAL.Insert(cn, user);

                    return Ok("Usuário criado com sucesso!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
