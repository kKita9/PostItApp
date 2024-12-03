using Microsoft.EntityFrameworkCore;
using PostApi.Models;

namespace PostApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Post> Posts { get; set; }
    }
}
