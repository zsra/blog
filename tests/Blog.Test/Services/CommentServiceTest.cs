using Blog.Core.Entities;
using Blog.Core.Interfaces.Services;
using Blog.Core.Services;
using Blog.Infrastructure.DataAccess;
using Blog.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Blog.Services.Test
{
    [TestClass]
    public class CommentServiceTest
    {
        BlogDbContext context;
        private DbContextOptions<BlogDbContext> options;
        private ICommentService commentService;

        [TestInitialize]
        public void RunBeforeEachTest()
        {
            options = new DbContextOptionsBuilder<BlogDbContext>()
                            .UseInMemoryDatabase(databaseName: "BlogDb")
                            .Options;

            context = new BlogDbContext(options);
            context.Posts.RemoveRange(context.Posts);
            context.Comments.RemoveRange(context.Comments);
            context.Posts.Add(new Post { Id = 1, Author = null, Body = "Lorum ipsum", 
                    Comments = new List<Comment>() { 
                        new Comment { Id = 1, Author = null, Body = "Ipsum lorum", CreatedAt = DateTime.Now, 
                            Comments = new List<Comment>() {  new Comment { Id = 2, Author = null, Body = "Ipsum lorum 2", CreatedAt = DateTime.Now,
                            Comments = new List<Comment>() { } }, new Comment { Id = 3, Author = null, Body = "Ipsum lorum 3", CreatedAt = DateTime.Now,
                            Comments = new List<Comment>() { } } } } }
            , Categories = new List<Category>(), CreatedAt = DateTime.Now, Title = "First" });
            
            context.SaveChanges();

            commentService = new CommentService(new AsyncRepository<Comment>(context));
        }

        [TestMethod]
        public void GetTest()
        {
            Comment comment = commentService.GetCommentById(1).Result;
            Assert.IsNotNull(comment);
            Assert.AreEqual(1, comment.Id);
            Assert.AreEqual("Ipsum lorum", comment.Body);
        }

        [TestMethod]
        public void CreateTest()
        {
            Comment comment = new()
            {
                Body = "sport",
                CreatedAt = DateTime.Now,
                
            };

            Comment result = commentService.Create(comment).Result;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            Assert.AreEqual(comment.Body, result.Body);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Comment comment = commentService.GetCommentById(1).Result;
            comment.Body = "Updated";

            Comment result = commentService.Update(comment).Result;

            Assert.IsNotNull(result);
            Assert.AreEqual(comment.Id, result.Id);
            Assert.AreEqual(comment.Body, result.Body);
        }

        [TestMethod]
        public void DeleteTest()
        {
            commentService.DeleteById(1);
            Assert.AreEqual(2, context.Comments.CountAsync().Result);
        }
    }
}
