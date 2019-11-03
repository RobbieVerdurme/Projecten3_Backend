using System.ComponentModel.DataAnnotations;

namespace Projecten3_Backend.Controllers
{
    /// <summary>
    /// This DTO is the payload for a login request.
    /// </summary>
    public class LoginDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}