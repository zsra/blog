using Blog.Core.Entities;
using Blog.Web.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Web.Converters
{
    public static class PostConverter
    {
        public static PostDTO EntityToDto(this Post post) => new()
        {
            Id = post.Id,
            Body = post.Body,
            Title = post.Title,
            AuthorId = post.Author == null ? int.MinValue : post.Author.Id,
            CreatedAt = post.CreatedAt,
            Categories = post.Categories == null ? new List<CategoryDTO>() :  new List<CategoryDTO>(post.Categories?.Select(_category => _category.EntityToDto())),
            Comments = post.Comments == null ? new List<CommentDTO>() : new List<CommentDTO>(post.Comments?.Select( comment => comment.EntityToDto() ))
        };

        public static Post DtoToEntity(this PostDTO postDTO, IEnumerable<User> users) => new()
        {
            Id = postDTO.Id,
            Author = users.FirstOrDefault(_user => _user.Id == postDTO.AuthorId),
            Body = postDTO.Body,
            Categories = new List<Category>(postDTO.Categories?.Select(_category => _category.DtoToEntity())),
            CreatedAt = postDTO.CreatedAt,
            Title = postDTO.Title,
            Comments = new List<Comment>(postDTO.Comments?.Select(_comment => _comment.DtoToEntity(users)))
        };
    }
}
