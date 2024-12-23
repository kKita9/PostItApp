using DataAccess.Data;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostApi.DTO;

[Route("api/[controller]")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PostController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("user-posts")]
    [Authorize]
    public IActionResult GetUserPosts()
    {
        var userId = int.Parse(User.FindFirst("id").Value);

        var posts = _context.Posts
            .Where(p => p.UserId == userId)
            .Select(p => new PostDto
            {
                Id = p.Id,
                Content = p.Content,
                CreatedAt = p.CreatedAt,
                AuthorName = $"{p.User.FirstName} {p.User.LastName}",
                LikeCount = p.Likes.Count
            })
            .ToList();

        return Ok(posts);
    }

    [HttpGet("user-and-friends-posts")]
    [Authorize]
    public IActionResult GetUserAndFriendsPosts()
    {
        var userId = int.Parse(User.FindFirst("id").Value);

        var friendsIds = _context.Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Friends.Select(f => f.Id))
            .ToHashSet();
        friendsIds.Add(userId);

        var posts = _context.Posts
            .Where(p => friendsIds.Contains(p.UserId))
            .Select(p => new PostDto
            {
                Id = p.Id,
                Content = p.Content,
                CreatedAt = p.CreatedAt,
                AuthorName = $"{p.User.FirstName} {p.User.LastName}",
                LikeCount = p.Likes.Count
            })
            .ToList();

        return Ok(posts);
    }

    [HttpGet("post-count")]
    [Authorize]
    public IActionResult GetPostCount()
    {
        var userId = int.Parse(User.FindFirst("id").Value);

        var postCount = _context.Posts.Count(p => p.UserId == userId);

        return Ok(postCount);
    }

    [HttpGet("{postId}/like-count")]
    public IActionResult GetLikeCount(int postId)
    {
        var likeCount = _context.PostLikes.Count(l => l.PostId == postId);

        return Ok(likeCount);
    }

    [HttpPost("like")]
    [Authorize]
    public IActionResult LikePost([FromBody] LikePostDto dto)
    {
        var userId = int.Parse(User.FindFirst("id").Value);

        var post = _context.Posts.FirstOrDefault(p => p.Id == dto.PostId);
        if (post == null)
        {
            return NotFound("Post not found.");
        }

        var alreadyLiked = _context.PostLikes.Any(l => l.PostId == dto.PostId && l.UserId == userId);
        if (alreadyLiked)
        {
            return BadRequest("Post already liked.");
        }

        _context.PostLikes.Add(new PostLike
        {
            PostId = dto.PostId,
            UserId = userId,
            LikedAt = DateTime.UtcNow
        });

        _context.SaveChanges();

        return Ok("Post liked successfully.");
    }

    [HttpGet("average-likes")]
    [Authorize]
    public IActionResult GetAverageLikes()
    {
        var userId = int.Parse(User.FindFirst("id").Value);

        var userPosts = _context.Posts
            .Where(p => p.UserId == userId)
            .Include(p => p.Likes) 
            .ToList();

        var averageLikes = userPosts.Any()
            ? userPosts.Average(p => p.Likes.Count)
            : 0;

        return Ok(new
        {
            PostCount = userPosts.Count,
            AverageLikes = averageLikes
        });
    }

    [HttpPost("add")]
    [Authorize]
    public IActionResult AddPost([FromBody] CreatePostDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Content))
        {
            return BadRequest("Content cannot be empty.");
        }

        var userId = int.Parse(User.FindFirst("id").Value);

        var newPost = new Post
        {
            Content = dto.Content,
            CreatedAt = DateTime.UtcNow,
            UserId = userId
        };

        _context.Posts.Add(newPost);
        _context.SaveChanges();

        return Ok(new
        {
            Message = "Post created successfully.",
            PostId = newPost.Id,
            CreatedAt = newPost.CreatedAt
        });
    }

    [HttpDelete("delete/{postId}")]
    [Authorize]
    public IActionResult DeletePost(int postId)
    {
        var userId = int.Parse(User.FindFirst("id").Value);
        var post = _context.Posts.Include(p => p.Likes).FirstOrDefault(p => p.Id == postId);

        if (post == null)
        {
            return NotFound("Post not found.");
        }

        if (post.UserId != userId)
        {
            return Forbid("You are not authorized to delete this post.");
        }

        _context.PostLikes.RemoveRange(post.Likes);

        _context.Posts.Remove(post);
        _context.SaveChanges();

        return Ok(new { Message = "Post deleted successfully." });
    }
}
