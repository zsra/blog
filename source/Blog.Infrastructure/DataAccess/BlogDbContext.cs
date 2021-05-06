using Blog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Blog.Infrastructure.DataAccess
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext([NotNullAttribute] DbContextOptions<BlogDbContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            ConfigureModelBuilder(builder);
            base.OnModelCreating(builder);

        }

        private void ConfigureModelBuilder(ModelBuilder builder)
        {
        }
    }
}
