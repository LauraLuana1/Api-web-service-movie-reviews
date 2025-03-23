using Api_web_service_movie_reviews.Models;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api_web_service_movie_reviews.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var model = await _context.Usuarios.ToListAsync();
            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(UsuarioDTO model)
        {

            Usuario novo = new Usuario()
            {
                Nome = model.Nome,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Perfil = model.Perfil
            };

            _context.Usuarios.Add(novo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = novo.Id }, novo);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> GetById(int Id)
        {
            var model = await _context.Usuarios
                  .FirstOrDefaultAsync(C => C.Id == Id);

            if (model == null) return NotFound();

            GerarLinks(model);
            return Ok(model);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> Update(int Id, UsuarioDTO model)
        {
            if (Id != model.Id) return BadRequest();

            var modelDb = await _context.Usuarios.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == Id);

            if (modelDb == null) return NotFound();

            modelDb.Nome = model.Nome;
            modelDb.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            modelDb.Perfil = model.Perfil;

            _context.Usuarios.Update(modelDb);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var model = await _context.Usuarios.FindAsync(Id);
            if (model == null) return NotFound();

            _context.Usuarios.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<ActionResult> Authenticate(AuthenticateDTO model)
        {
            var usuarioDb = await _context.Usuarios.FindAsync(model.Id);

            if (usuarioDb == null || !BCrypt.Net.BCrypt.Verify(model.Password, usuarioDb.Password))
                return Unauthorized();

            var jwt = GenerateJwtToken(usuarioDb);

            return Ok(new { jwtToken = jwt });
        }

        private string GenerateJwtToken(Usuario model)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("Ry74cBQva5dThwbwchR9jhbtRFnJxWSZ");
            var claims = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, model.Id.ToString()),
                new Claim(ClaimTypes.Role, model.Perfil.ToString())
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private void GerarLinks(Usuario model)
        {
            model.Links.Add(new LinkDTO(model.Id, Url.ActionLink(), rel: "self", metodo: "GET"));
            model.Links.Add(new LinkDTO(model.Id, Url.ActionLink(), rel: "update", metodo: "PUT"));
            model.Links.Add(new LinkDTO(model.Id, Url.ActionLink(), rel: "delete", metodo: "Delete"));
        }
    }
}
