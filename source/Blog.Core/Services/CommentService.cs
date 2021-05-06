using Blog.Core.Entities;
using Blog.Core.Interfaces;
using Blog.Core.Interfaces.Services;
using System.Threading.Tasks;

namespace Blog.Core.Services
{
    public class CommentService : ICommentService
    {
        private readonly IAsyncRepository<Comment> _commentRepository;

        public CommentService(IAsyncRepository<Comment> commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async ValueTask<Comment> Create(Comment comment)
        {
            return await _commentRepository.AddAsync(comment);
        }

        public async ValueTask DeleteById(int commentId)
        {
            await _commentRepository.DeleteAsync(commentId);
        }

        public async ValueTask<Comment> GetCommentById(int commentId)
        {
            return await _commentRepository.GetByIdAsync(commentId);
        }

        public async ValueTask<Comment> Update(Comment comment)
        {
            return await _commentRepository.UpdateAsync(comment);
        }
    }
}
