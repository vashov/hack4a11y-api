using System.ComponentModel.DataAnnotations;

namespace Api.Controllers.Auth.Models
{
    public class LoginRequest
    {
        [Required]
        public long Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
