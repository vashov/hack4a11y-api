using System.Collections.Generic;

namespace Api.Controllers.Auth.Models
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public long UserId { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }
}
