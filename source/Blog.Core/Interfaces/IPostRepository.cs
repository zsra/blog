using Blog.Core.Entities;
using System.Threading.Tasks;

namespace Blog.Core.Interfaces
{
    public interface IPostRepository
    {
        Task<Post> GetPostById(int id);
    }
}
