using Microsoft.EntityFrameworkCore;

namespace Api_web_service_movie_reviews.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {    
        }
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }
    }
}
