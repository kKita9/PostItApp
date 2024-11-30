using Microsoft.AspNetCore.Mvc;
using PeopleApi.Data;
using PeopleApi.Dto;
using PeopleApi.Models;

namespace PeopleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FriendsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // get all friends 
        [HttpGet]
        public IActionResult GetFriends()
        {
            var friends = _context.Friends
                .Select(f => new FriendDto
                {
                    Id = f.Id,
                    Name = f.Name,
                    Email = f.Email
                })
                .ToList();

            return Ok(friends);
        }

        // add new friend 
        [HttpPost]
        public IActionResult AddFriend([FromBody] CreateFriendDto createFriendDto)
        {
            var friend = new Friend
            {
                Name = createFriendDto.Name,
                Email = createFriendDto.Email
            };

            _context.Friends.Add(friend);
            _context.SaveChanges();

            var friendDto = new FriendDto
            {
                Id = friend.Id,
                Name = friend.Name,
                Email = friend.Email
            };

            return CreatedAtAction(nameof(GetFriends), new { id = friend.Id }, friendDto);
        }

        // remove friend 
        [HttpDelete("{id}")]
        public IActionResult DeleteFriend(int id)
        {
            var friend = _context.Friends.FirstOrDefault(f => f.Id == id);
            if (friend == null)
            {
                return NotFound();
            }

            _context.Friends.Remove(friend);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
