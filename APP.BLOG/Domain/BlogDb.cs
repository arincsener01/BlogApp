using Microsoft.EntityFrameworkCore;

namespace APP.BLOG.Domain
{
    public class BlogDb : DbContext
    {
        public DbSet<BlogTag> BlogTags { get; set; }
        public BlogDb(DbContextOptions<BlogDb> options) : base(options)
        {
        }
    }
}
