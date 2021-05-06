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
    public class CategoryServiceTest
    {
        BlogDbContext context;
        private DbContextOptions<BlogDbContext> options;
        private ICategoryService categoryService;

        [TestInitialize]
        public void RunBeforeEachTest()
        {
            options = new DbContextOptionsBuilder<BlogDbContext>()
                            .UseInMemoryDatabase(databaseName: "BlogDb")
                            .Options;

            context = new BlogDbContext(options);
            context.Posts.RemoveRange(context.Posts);
            context.Categories.RemoveRange(context.Categories);
            context.Posts.Add(new Post { Id = 1, Author = null, Body = "Lorum ipsum", Comments = new List<Comment>(), Categories = new List<Category>() { 
                new Category { Id = 1, Name = "Sport", SerializedAgeGroups = AgeGroup.ADULT.ToJson() } }
            , CreatedAt = DateTime.Now, Title = "First" });
            context.Posts.Add(new Post { Id = 2, Author = null, Body = "Lorum ipsum", Comments = new List<Comment>(), Categories = new List<Category>() { 
                new Category { Id = 2, Name = "Game", SerializedAgeGroups = (new AgeGroup[] { AgeGroup.YOUNG, AgeGroup.ADULT }).ToJson() } }
            , CreatedAt = DateTime.Now.AddDays(-1), Title = "Second" });
            context.SaveChanges();

            categoryService = new CategoryService(new AsyncRepository<Category>(context));
        }

        [TestMethod]
        public void CreateTest()
        {
            Category category = new()
            {
                Name = "New",
                SerializedAgeGroups = (new AgeGroup[] { AgeGroup.ADULT, AgeGroup.ELDER }).ToJson()
            };

            Category result = categoryService.Create(category).Result;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            Assert.AreEqual(category.Name, result.Name);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Category category = categoryService.GetCategoryById(1).Result;
            Assert.IsNotNull(category);

            category.Name = "Public";

            Category result = categoryService.Update(category).Result;
            Assert.IsNotNull(result);
            Assert.AreEqual(category.Name, result.Name);
        }

        [TestMethod]
        public void DeleteTest()
        {
            categoryService.DeleteById(1);
            Assert.AreEqual(1, context.Categories.CountAsync().Result);
        }

        [TestMethod]
        public void GetAllTest()
        {
            IEnumerable<Category> categories = categoryService.GetAll().Result;

            Assert.AreEqual(2, categories.Count());
            Assert.AreEqual(2, categories.ElementAt(1).Id);
            Assert.AreEqual(1, categories.ElementAt(0).Id);
        }
    }
}
