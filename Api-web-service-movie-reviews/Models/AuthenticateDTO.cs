using System.ComponentModel.DataAnnotations;

namespace Api_web_service_movie_reviews.Models
{
    public class AuthenticateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
