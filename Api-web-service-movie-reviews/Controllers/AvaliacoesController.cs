using Api_web_service_movie_reviews.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_web_service_movie_reviews.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvaliacoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AvaliacoesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var model = await _context.Avaliacoes.ToListAsync();
            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Avaliacao model)
        {

            _context.Avaliacoes.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = model.Id }, model);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> GetById(int Id)
        {
            var model = await _context.Avaliacoes
                  .FirstOrDefaultAsync(C => C.Id == Id);
            if (model == null) return NotFound();
            return Ok(model);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> Update(int Id, Avaliacao model)
        {
            if (Id != model.Id) return BadRequest();

            var modelDb = await _context.Avaliacoes.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == Id);

            if (modelDb == null) return NotFound();

            _context.Avaliacoes.Update(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var model = await _context.Avaliacoes.FindAsync(Id);
            if (model == null) return NotFound();

            _context.Avaliacoes.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
