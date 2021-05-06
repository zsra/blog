using Blog.Core.Entities;
using Blog.Core.Interfaces.Services;
using Blog.Web.Converters;
using Blog.Web.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Web.Controllers
{
    [ApiController]
    [Route("api/v1/public/comment")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;

        public CommentController(ICommentService commentService, IUserService userService)
        {
            _commentService = commentService;
            _userService = userService;
        }

        [HttpGet("{commentId}")]
        public async ValueTask<CommentDTO> Get(int commentId)
        {
            Comment comment = await _commentService.GetCommentById(commentId);
            return comment.EntityToDto();
        }

        [HttpPut("create")]
        public async ValueTask<CommentDTO> Create([FromBody]CommentDTO comment)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<User> users = await _userService.GetAll();
                Comment result = await _commentService.Create(comment.DtoToEntity(users));
                return result.EntityToDto();
            }

            return comment;
        }

        [HttpPut("update")]
        public async ValueTask<CommentDTO> Update([FromBody] CommentDTO comment)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<User> users = await _userService.GetAll();
                Comment result = await _commentService.Update(comment.DtoToEntity(users));
                return result.EntityToDto();
            }

            return comment;
        }

        [HttpDelete("{commentId}")]
        public async ValueTask<string> Delete(int commentId)
        {
            await _commentService.DeleteById(commentId);
            return commentId.ToString();
        }
    }
}
