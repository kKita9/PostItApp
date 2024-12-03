using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostApi.Data;
using PostApi.DTO;
using PostApi.Models;

namespace PostApi.Controllers
{
    [Authorize] // Wymaga autoryzacji JWT
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PostController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Post
        [HttpGet]
        public IActionResult GetPosts()
        {
            var posts = _context.Posts.ToList();
            return Ok(posts);
        }

        // GET: api/Post/{id}
        [HttpGet("{id}")]
        public IActionResult GetPost(int id)
        {
            var post = _context.Posts.Find(id);
            if (post == null)
                return NotFound();
            return Ok(post);
        }

        // POST: api/Post
        [HttpPost]
        public IActionResult CreatePost(CreatePostDto createPostDto)
        {
            // Tworzenie nowego obiektu Post na podstawie DTO
            var post = new Post
            {
                Title = createPostDto.Title,
                Content = createPostDto.Content,
                CreatedAt = DateTime.UtcNow
            };

            // Dodawanie posta do bazy danych
            _context.Posts.Add(post);
            _context.SaveChanges();

            // Zwracanie stworzonego posta z automatycznie wygenerowanym Id
            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
        }

        // PUT: api/Post/{id}
        [HttpPut("{id}")]
        public IActionResult UpdatePost(int id, Post post)
        {
            var existingPost = _context.Posts.Find(id);
            if (existingPost == null)
                return NotFound();

            existingPost.Title = post.Title;
            existingPost.Content = post.Content;
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/Post/{id}
        [HttpDelete("{id}")]
        public IActionResult DeletePost(int id)
        {
            var post = _context.Posts.Find(id);
            if (post == null)
                return NotFound();

            _context.Posts.Remove(post);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
