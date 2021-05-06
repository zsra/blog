using Blog.Core.Entities;
using Blog.Core.Extensions;
using Blog.Core.Interfaces.Services;
using Blog.Web.Converters;
using Blog.Web.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Controllers
{
    [ApiController]
    [Route("api/v1/public/post")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IUserService _userService;

        public PostController(IPostService postService, IUserService userService)
        {
            _postService = postService;
            _userService = userService;
        }

        [HttpPost("all")]
        public async ValueTask<IEnumerable<PostDTO>> GetAll([FromBody]string ageGroup)
        {
            _ = Enum.TryParse(ageGroup, out AgeGroup _ageGroup);
            IEnumerable<Post> posts = await _postService.GetAllByAgeGroup(_ageGroup);
            return posts.Select( post => post.EntityToDto());
        }

        [HttpGet("{postId}")]
        public async ValueTask<PostDTO> Get(int postId)
        {
            Post post = await _postService.GetPostById(postId);
            return post.EntityToDto();
        }

        [HttpPost("create")]
        public async ValueTask<PostDTO> Create([FromBody] PostDTO post)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<User> users = await _userService.GetAll();
                Post result = await _postService.Create(post.DtoToEntity(users));
                return result.EntityToDto();
            }

            return post;
        }

        [HttpPut("update")]
        public async ValueTask<PostDTO> Update([FromBody] PostDTO post)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<User> users = await _userService.GetAll();
                Post result = await _postService.Update(post.DtoToEntity(users));
                return result.EntityToDto();
            }

            return post;
        }

        [HttpDelete("{postId}")]
        public async ValueTask<string> Delete(int postId)
        {
            await _postService.DeleteById(postId);
            return postId.ToString();
        }
    }
}
