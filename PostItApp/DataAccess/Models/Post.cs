using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Post
    {
        public int Id { get; set; } 

        [Required]
        [MaxLength(1000)]
        public string Content { get; set; } 

        public DateTime CreatedAt { get; set; } 
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<PostLike> Likes { get; set; }
    }
}
