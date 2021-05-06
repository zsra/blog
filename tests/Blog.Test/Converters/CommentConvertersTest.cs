using Blog.Core.Entities;
using Blog.Web.Converters;
using Blog.Web.DTOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Test.Converters
{
    [TestClass]
    public class CommentConvertersTest
    {
        [TestMethod]
        public void EntityToDto()
        {
            Comment comment = new()
            {
                Author = new User { Id = 1 },
                Body = "body",
                Comments = new List<Comment>
                {
                    new Comment
                    {
                        Author = new User { Id = 2 },
                        Body = "body",
                        Id = 2,
                        CreatedAt = DateTime.Now
                    },
                    new Comment
                    {
                        Author = new User { Id = 3 },
                        Body = "body",
                        Id = 3,
                        CreatedAt = DateTime.Now
                    }
                },
                Id = 1,
                CreatedAt = DateTime.Now,
            };

            CommentDTO commentDTO = comment.EntityToDto();
            Assert.IsNotNull(commentDTO);
            Assert.AreEqual(comment.Id, commentDTO.Id);
            Assert.AreEqual(comment.Body, commentDTO.Body);
            Assert.AreEqual(comment.CreatedAt, commentDTO.CreatedAt);
            Assert.AreEqual(comment.Comments.First().Id, commentDTO.Comments.First().Id);
            Assert.AreEqual(comment.Comments.Last().Id, commentDTO.Comments.Last().Id);
        }

        [TestMethod]
        public void DtoToEntity()
        {
            List<User> users = new()
            {
                new User { Id = 1 },
                new User { Id = 2 },
                new User { Id = 3 },
            };

            CommentDTO commentDTO = new()
            {
                Id = 1,
                AuthorId = 1,
                Body = "body",
                CreatedAt = DateTime.Now,
                Comments = new List<CommentDTO>
                {
                    new CommentDTO
                    {
                        Id = 2,
                        CreatedAt = DateTime.Now,
                        AuthorId = 2,
                        Body = "first"
                    },
                     new CommentDTO
                    {
                        Id = 3,
                        CreatedAt = DateTime.Now,
                        AuthorId = 2,
                        Body = "second"
                    }
                }
            };

            Comment comment = commentDTO.DtoToEntity(users);
            Assert.IsNotNull(comment);
            Assert.AreEqual(commentDTO.Id, comment.Id);
            Assert.AreEqual(commentDTO.AuthorId, comment.Author.Id);
            Assert.AreEqual(commentDTO.Body, comment.Body);
            Assert.AreEqual(commentDTO.CreatedAt, comment.CreatedAt);
            Assert.AreEqual(commentDTO.Comments.First().Id, comment.Comments.First().Id);
            Assert.AreEqual(commentDTO.Comments.Last().Id, comment.Comments.Last().Id);
        }
    }
}
