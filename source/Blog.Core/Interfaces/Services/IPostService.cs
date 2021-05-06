using Blog.Core.Entities;
using Blog.Core.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Core.Interfaces.Services
{
    public interface IPostService
    {
        ValueTask<IEnumerable<Post>> GetAllByAgeGroup(AgeGroup ageGroup);
        ValueTask<Post> GetPostById(int postId);
        ValueTask<Post> Create(Post post);
        ValueTask<Post> Update(Post post);
        ValueTask DeleteById(int postId);
    }
}
