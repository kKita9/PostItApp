using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleApi.Data;
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

        // Pobierz wszystkich znajomych
        [HttpGet]
        [Authorize]
        public IActionResult GetFriends()
        {
            var friends = _context.Friends.ToList();
            return Ok(friends);
        }

        // Dodaj nowego znajomego
        [HttpPost]
        [Authorize]
        public IActionResult AddFriend([FromBody] Friend friend)
        {
            _context.Friends.Add(friend);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetFriends), new { id = friend.Id }, friend);
        }

        // Usuń znajomego
        [HttpDelete("{id}")]
        [Authorize]
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
