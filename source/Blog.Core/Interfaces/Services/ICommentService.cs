using Blog.Core.Entities;
using System.Threading.Tasks;

namespace Blog.Core.Interfaces.Services
{
    public interface ICommentService
    {
        ValueTask<Comment> GetCommentById(int commentId);
        ValueTask<Comment> Create(Comment comment);
        ValueTask<Comment> Update(Comment comment);
        ValueTask DeleteById(int commentId);
    }
}
