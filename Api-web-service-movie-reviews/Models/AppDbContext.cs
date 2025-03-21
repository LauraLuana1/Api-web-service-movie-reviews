using Microsoft.EntityFrameworkCore;

namespace Api_web_service_movie_reviews.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {    
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<FilmeUsuarios>()
                .HasKey(c => new {c.FilmeId, c.UsuarioId});

            builder.Entity<FilmeUsuarios>()
                .HasOne(c => c.Filme).WithMany(c => c.Usuarios)
                .HasForeignKey(c => c.FilmeId);

            builder.Entity<FilmeUsuarios>()
                .HasOne(c => c.Usuario).WithMany(c => c.Filmes)
                .HasForeignKey(c => c.UsuarioId);
        }

        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<FilmeUsuarios> FilmesUsuarios { get; set; }
    }
}
