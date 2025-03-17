using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api_web_service_movie_reviews.Models
{
    [Table("Filmes")]
    public class Filme
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Genero { get; set; }
        [Required]
        public string Sinopse { get; set; }
        [Required]
        public int AnoLancamento { get; set; }
        [Required]
        public decimal Nota { get; set; }
        public ICollection<Avaliacao> Avaliações { get; set; }

    }
}
