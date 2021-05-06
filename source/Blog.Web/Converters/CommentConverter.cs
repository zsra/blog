using Blog.Core.Entities;
using Blog.Web.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Web.Converters
{
    public static class CommentConverter
    {
        public static CommentDTO EntityToDto(this Comment comment) => new()
        {
            Id = comment.Id,
            Body = comment.Body,
            CreatedAt = comment.CreatedAt,
            AuthorId = comment.Author == null ? int.MinValue : comment.Author.Id,
            AuthorName = comment.Author == null ? string.Empty : comment.Author.Username,
            Comments = comment.Comments == null ? new List<CommentDTO>() : new List<CommentDTO>(comment.Comments?.Select(_comment => EntityToDto(_comment))),
        };

        public static Comment DtoToEntity(this CommentDTO commentDTO, IEnumerable<User> users) => new()
        {
            Id = commentDTO.Id,
            Author = users.FirstOrDefault(_user => _user.Id == commentDTO.AuthorId),
            Body = commentDTO.Body,
            Comments = commentDTO.Comments == null ? new List<Comment>() : new List<Comment>(commentDTO.Comments?.Select(_comment => DtoToEntity(_comment, users))),
            CreatedAt = commentDTO.CreatedAt
        };
    }
}
