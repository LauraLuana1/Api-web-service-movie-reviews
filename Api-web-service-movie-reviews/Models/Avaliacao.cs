using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api_web_service_movie_reviews.Models
{
    [Table("Avaliações")]
    public class Avaliacao
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Resenha { get; set; }
        [Required]
        public DateTime DataAvaliacao { get; set; }
        [Required]
        public int FilmeId { get; set; }
        public Filme Filme { get; set; }
    }
}
