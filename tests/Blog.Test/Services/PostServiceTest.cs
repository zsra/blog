using Blog.Core.Entities;
using Blog.Core.Extensions;
using Blog.Core.Interfaces.Services;
using Blog.Core.Services;
using Blog.Infrastructure.DataAccess;
using Blog.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Services.Test
{
    [TestClass]
    public class PostServiceTest
    {
        BlogDbContext context;
        private DbContextOptions<BlogDbContext> options;
        private IPostService postService;

        [TestInitialize]
        public void RunBeforeEachTest()
        {
            options = new DbContextOptionsBuilder<BlogDbContext>()
                            .UseInMemoryDatabase(databaseName: "BlogDb")
                            .Options;

            context = new BlogDbContext(options);
            context.Posts.RemoveRange(context.Posts);
            context.Categories.RemoveRange(context.Categories);
            context.Posts.Add(new Post { Id = 1, Author = null, Body = "Lorum ipsum", Comments = new List<Comment>(), Categories = new List<Category>(), CreatedAt = DateTime.Now, Title = "First" });
            context.Posts.Add(new Post { Id = 2, Author = null, Body = "Lorum ipsum", Comments = new List<Comment>(), Categories = new List<Category>(), CreatedAt = DateTime.Now.AddDays( -1), Title = "Second" });
            context.Posts.Add(new Post { Id = 3, Author = null, Body = "Lorum ipsum", Comments = new List<Comment>(), Categories = new List<Category>() { new Category { Id = 1, Name = "Game", SerializedAgeGroups = (new AgeGroup[] { AgeGroup.YOUNG, AgeGroup.ADULT }).ToJson() } }, CreatedAt = DateTime.Now.AddDays(1), Title = "Third" });
            context.SaveChanges();

            postService = new PostService(new AsyncRepository<Post>(context), new PostRepository(context));
        }

        [TestMethod]
        public void CreateTest()
        {
            Post post = new()
            {
                Title = "New",
                Author = null,
                Body = "New body",
                Categories = new List<Category>(),
                Comments = new List<Comment>(),
                CreatedAt = DateTime.Now,
            };

            Post result = postService.Create(post).Result;

            Assert.IsNotNull(result);
            Assert.IsNotNull(post.Id);
            Assert.AreEqual(post.Title, result.Title);
            Assert.AreEqual(post.Body, result.Body);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Post post = postService.GetPostById(1).Result;
            post.Title = "Update";

            Post result = postService.Update(post).Result;

            Assert.IsNotNull(result);
            Assert.AreEqual(post.Id, result.Id);
            Assert.AreEqual(post.Title, result.Title);
            Assert.AreEqual(post.Body, result.Body);
        }

        [TestMethod]
        public void DeleteTest()
        {
            Post post = new()
            {
                Title = "New",
                Author = null,
                Body = "New body",
                Categories = new List<Category>(),
                Comments = new List<Comment>(),
                CreatedAt = DateTime.Now,
            };

            Post result = postService.Create(post).Result;

            postService.DeleteById(result.Id);
            IEnumerable<Post> posts = postService.GetAllByAgeGroup(AgeGroup.DEFAULT).Result;
            
            Assert.AreEqual(3, posts.Count());
            Assert.AreEqual(2, posts.ElementAt(2).Id);
            Assert.AreEqual(1, posts.ElementAt(1).Id);
            Assert.AreEqual(3, posts.ElementAt(0).Id);
        }

        [TestMethod]
        public void GetTest()
        {
            Post post = postService.GetPostById(1).Result;

            Assert.IsNotNull(post);
            Assert.AreEqual("First", post.Title);
        }

        [TestMethod]
        public void GetAllTest()
        {
            IEnumerable<Post> posts = postService.GetAllByAgeGroup(AgeGroup.DEFAULT).Result;

            Assert.AreEqual(3, posts.Count());
            Assert.AreEqual(2, posts.ElementAt(2).Id);
            Assert.AreEqual(1, posts.ElementAt(1).Id);
            Assert.AreEqual(3, posts.ElementAt(0).Id);
        }

        [TestMethod]
        public void GetAllByAdultTest()
        {
            IEnumerable<Post> posts = postService.GetAllByAgeGroup(AgeGroup.ADULT).Result;

            Assert.AreEqual(1, posts.Count());
            Assert.AreEqual(3, posts.ElementAt(0).Id);
        }
    }
}
