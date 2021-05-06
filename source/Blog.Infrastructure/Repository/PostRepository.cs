using Blog.Core.Entities;
using Blog.Core.Interfaces;
using Blog.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Repository
{
    public class PostRepository : AsyncRepository<Post>, IPostRepository
    {
        public PostRepository(BlogDbContext context) : base (context) { }

        public async Task<Post> GetPostById(int id)
        {
            IEnumerable<Comment> comments = await  _context.Comments.Include( c => c.Comments).ToListAsync();
            Post post = await _context.Posts.Include(p => p.Categories).Include(p => p.Comments).FirstOrDefaultAsync( p => p.Id == id);
            post.Comments = new List<Comment>(comments.Where(c => c.Post?.Id == post.Id));

            return post;
        }
    }
}
