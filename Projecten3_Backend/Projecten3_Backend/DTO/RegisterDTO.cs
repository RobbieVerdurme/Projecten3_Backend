using System.ComponentModel.DataAnnotations;

namespace Projecten3_Backend.Controllers
{
    /// <summary>
    /// This DTO is the payload for a registration request.
    /// </summary>
    public class RegisterDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}