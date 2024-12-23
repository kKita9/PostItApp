using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace IdentityApi.DTO
{
    public class RegisterUserDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }

}
