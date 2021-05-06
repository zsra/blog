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
    public class PostConvertersTest
    {
        [TestMethod]
        public void EntityToDto()
        {
            Post post = new()
            {
                Id = 1,
                Author = new User { Id = 1 },
                Body = "Lorum ipsum",
                Categories = new List<Category> { new Category { Id = 1 } },
                Comments = new List<Comment> { new Comment { Id = 1, Author = new User { Id = 1 }, Body = "comment" } },
                CreatedAt = DateTime.Now,
                Title = "Title"
            };

            PostDTO postDTO = post.EntityToDto();
            Assert.IsNotNull(postDTO);
            Assert.AreEqual(post.Id, postDTO.Id);
            Assert.AreEqual(post.Author.Id, postDTO.AuthorId);
            Assert.AreEqual(post.Body, postDTO.Body);
            Assert.AreEqual(post.CreatedAt, postDTO.CreatedAt);
            Assert.AreEqual(post.Categories.First().Id, postDTO.Categories.First().Id);
            Assert.AreEqual(post.Comments.First().Id, postDTO.Comments.First().Id);
            Assert.AreEqual(post.Title, postDTO.Title);
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


            PostDTO postDTO = new()
            {
                Id = 1,
                Categories = new List<CategoryDTO> { new CategoryDTO { Id = 1 } },
                AuthorId = 1,
                Body = "Lorum ipsum",
                Comments = new List<CommentDTO> { new CommentDTO { Id = 1, AuthorId = 2, Body = "comment" } },
                CreatedAt = DateTime.Now,
                Title = "Title"
            };


            Post post = postDTO.DtoToEntity(users);
            Assert.IsNotNull(post);
            Assert.AreEqual(postDTO.Id, post.Id);
            Assert.AreEqual(postDTO.AuthorId, post.Author.Id);
            Assert.AreEqual(postDTO.Body, post.Body);
            Assert.AreEqual(postDTO.CreatedAt, post.CreatedAt);
            Assert.AreEqual(postDTO.Title, post.Title);
            Assert.AreEqual(postDTO.Categories.First().Id, post.Categories.First().Id);
            Assert.AreEqual(postDTO.Comments.First().Id, post.Comments.First().Id);
        }
    }
}
