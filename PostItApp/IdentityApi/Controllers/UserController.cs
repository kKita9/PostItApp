//using DataAccess.Data;
//using DataAccess.Models;
//using IdentityApi.DTO;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace IdentityApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UsersController : ControllerBase
//    {
//        private readonly ApplicationDbContext _context;

//        public UsersController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        [HttpPost("register")]
//        public IActionResult Register([FromBody] RegisterUserDto dto)
//        {
//            if (_context.Users.Any(u => u.Username == dto.Username))
//            {
//                return BadRequest("Username already exists.");
//            }

//            var user = new User
//            {
//                Username = dto.Username,
//                Password = dto.Password
//            };

//            _context.Users.Add(user);
//            _context.SaveChanges();

//            return Ok("User registered successfully.");
//        }

//        [HttpPost("login")]
//        public IActionResult Login([FromBody] RegisterUserDto dto)
//        {
//            Console.WriteLine($"Login attempt: Username = {dto.Username}, Password = {dto.Password}");
//            var user = _context.Users.FirstOrDefault(u =>
//                u.Username == dto.Username && u.Password == dto.Password);

//            if (user == null)
//            {
//                return Unauthorized("Invalid credentials.");
//            }

//            // create JWT token
//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SuperSecretKey12345SuperSecretKey12345!"));
//            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//            var claims = new[]
//            {
//                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
//                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//                new Claim("id", user.Id.ToString())
//            };

//            var token = new JwtSecurityToken(
//                claims: claims,
//                expires: DateTime.UtcNow.AddHours(1),
//                signingCredentials: creds);

//            return Ok(new
//            {
//                token = new JwtSecurityTokenHandler().WriteToken(token)
//            });
//        }

//        [Authorize]
//        [HttpGet("profile")]
//        public IActionResult GetUserProfile()
//        {
//            var username = User.Identity?.Name;
//            return Ok(new { Username = username });
//        }
//    }
//}
