using System.ComponentModel.DataAnnotations;

namespace Api.Controllers.Users.Models
{
    public class UpdateUserRequest
    {
        [MaxLength(256)]
        public string About { get; set; }

        [MaxLength(128)]
        public string GivenName { get; set; }

        [MaxLength(128)]
        public string FamilyName { get; set; }
    }
}
