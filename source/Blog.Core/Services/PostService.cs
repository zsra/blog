using Blog.Core.Entities;
using Blog.Core.Extensions;
using Blog.Core.Interfaces;
using Blog.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IAsyncRepository<Post> _postRepository;
        private readonly IPostRepository _repository;

        public PostService(IAsyncRepository<Post> postRepository, IPostRepository repository)
        {
            _postRepository = postRepository;
            _repository = repository;
        }

        public async ValueTask<Post> Create(Post post)
        {
            post.CreatedAt = DateTime.Now;
            return await _postRepository.AddAsync(post);
        }

        public async ValueTask DeleteById(int postId)
        {
            await _postRepository.DeleteAsync(postId);
        }

        public async ValueTask<IEnumerable<Post>> GetAllByAgeGroup(AgeGroup ageGroup)
        {
            IEnumerable<Post> posts = await _postRepository.ListAllAsync();
            if (ageGroup == AgeGroup.DEFAULT)
                return posts.OrderByDescending(post => post.CreatedAt);

            return posts.Where( post => post.Categories.Any( c => c.GetAgeGroups().Contains(ageGroup)))
                .OrderByDescending( post => post.CreatedAt);
        }

        public async ValueTask<Post> GetPostById(int postId)
        {
            return await _repository.GetPostById(postId);
        }

        public async ValueTask<Post> Update(Post post)
        {
            return await _postRepository.UpdateAsync(post);
        }
    }
}
