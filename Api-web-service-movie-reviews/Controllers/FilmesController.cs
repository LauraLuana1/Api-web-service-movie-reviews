﻿using Api_web_service_movie_reviews.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_web_service_movie_reviews.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FilmesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var model = await _context.Filmes.ToListAsync();
            return Ok(model);
        }

        [HttpPost] 
        public async Task<ActionResult> Create(Filme model)
        {
            if (model.AnoLancamento <= 0)
            {
                return BadRequest(new { message = "Ano de Lançamento é obrigatório e deve ser maior do que zero" });
            }

            _context.Filmes.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = model.Id }, model);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> GetById(int Id)
        {
            var model = await _context.Filmes
                  .Include(t => t.Avaliacoes)
                  .FirstOrDefaultAsync(C => C.Id == Id);

            if (model == null) return NotFound();
            return Ok(model);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> Update(int Id, Filme model)
        {
            if (Id != model.Id) return BadRequest();

            var modelDb = await _context.Filmes.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == Id);

            if (modelDb == null) return NotFound();

            _context.Filmes.Update(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id)
        {
         var model = await _context.Filmes.FindAsync(Id);
            if (model == null) return NotFound();

            _context.Filmes.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
