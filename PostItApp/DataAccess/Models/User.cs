using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class User
    {
        public int Id { get; set; } 

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } 

        [Required]
        [EmailAddress]
        public string Email { get; set; } 

        [Required]
        [MinLength(6)]
        public string Password { get; set; } 

        public ICollection<Friend> Friends { get; set; }

        public ICollection<Post> Posts { get; set; }
        public ICollection<PostLike> LikedPosts { get; set; }
    }
}
