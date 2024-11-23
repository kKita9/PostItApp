using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace IdentityApi.DTO
{
    public class RegisterUserDto
    {
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
    }
}
