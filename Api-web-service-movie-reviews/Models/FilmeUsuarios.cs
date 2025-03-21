using System.ComponentModel.DataAnnotations.Schema;

namespace Api_web_service_movie_reviews.Models
{
    [Table("FilmeUsuarios")]
    public class FilmeUsuarios
    {
        public int FilmeId { get; set; }
        public Filme Filme { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
