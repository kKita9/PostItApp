using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Friend
    {
        public int Id { get; set; } 

        [Required]
        public int UserId { get; set; }
        public User User { get; set; } 

        [Required]
        public int FriendUserId { get; set; }
        public User FriendUser { get; set; } 

        public DateTime FriendshipStarted { get; set; } 

    }
}
