using System.ComponentModel.DataAnnotations;

namespace Api.Controllers.Auth.Models
{
    public enum AwailableRoles
    {
        Creator = 0,
        Executor = 1
    }

    public class RegisterRequest
    {
        [Required]
        public long Login { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        public AwailableRoles Role { get; set; }
    }
}
