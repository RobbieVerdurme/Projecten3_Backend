using System.ComponentModel.DataAnnotations;

namespace Projecten3_Backend.Controllers
{
    public class RegisterDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}