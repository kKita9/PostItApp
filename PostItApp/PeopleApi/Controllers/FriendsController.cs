using DataAccess.Data;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeopleApi.Dto;

namespace PeopleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FriendsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FriendsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("list")]
        public IActionResult GetFriends()
        {
            var userId = int.Parse(User.FindFirst("id").Value);

            var friends = _context.Users
                .Include(u => u.Friends)
                .FirstOrDefault(u => u.Id == userId)?
                .Friends
                .Select(f => new FriendDto
                {
                    Id = f.Id,
                    FirstName = f.FirstName,
                    LastName = f.LastName,
                    Email = f.Email
                })
                .ToList();

            if (friends == null || !friends.Any())
            {
                return Ok(new { Message = "No friends found." });
            }

            return Ok(friends);
        }

        [HttpGet("suggested")]
        public IActionResult GetSuggestedFriends()
        {
            var userId = int.Parse(User.FindFirst("id").Value);

            var user = _context.Users
                .Include(u => u.Friends)
                .FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var friendsIds = user.Friends.Select(f => f.Id).ToHashSet();
            friendsIds.Add(userId);

            var suggestedFriends = _context.Users
                .Where(u => !friendsIds.Contains(u.Id))
                .Select(u => new FriendDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email
                })
                .ToList();

            return Ok(suggestedFriends);
        }

        [HttpPost("add/{friendId}")]
        public IActionResult AddFriend(int friendId)
        {
            var userId = int.Parse(User.FindFirst("id").Value); 

            var user = _context.Users
                .Include(u => u.Friends)
                .FirstOrDefault(u => u.Id == userId);

            var friend = _context.Users.FirstOrDefault(u => u.Id == friendId);

            if (user == null || friend == null)
            {
                return NotFound("User or friend not found.");
            }

            if (user.Friends.Any(f => f.Id == friendId))
            {
                return BadRequest("This user is already your friend.");
            }

            user.Friends.Add(friend);
            _context.SaveChanges();

            return Ok("Friend added successfully.");
        }

        [HttpDelete("remove/{friendId}")]
        public IActionResult RemoveFriend(int friendId)
        {
            var userId = int.Parse(User.FindFirst("id").Value); 

            var user = _context.Users
                .Include(u => u.Friends)
                .FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var friend = user.Friends.FirstOrDefault(f => f.Id == friendId);

            if (friend == null)
            {
                return BadRequest("This user is not your friend.");
            }

            user.Friends.Remove(friend);
            _context.SaveChanges();

            return Ok("Friend removed successfully.");
        }
    }
}
