using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers.Users.Models
{
    public class UserResponse
    {
        public long Id { get; set; }
        public long Phone { get; set; }
        public List<string> Roles { get; set; }
    }
}
