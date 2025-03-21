using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Api_web_service_movie_reviews.Models
{
    public class UsuarioDTO
    {
        public int? Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public Perfil Perfil { get; set; }
    }
}
