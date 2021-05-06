using Blog.Core.Accounts;
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

namespace Blog.Test.Services
{
    [TestClass]
    public class UserServiceTest
    {
        BlogDbContext blogDbContext;
        ApplicationDbContext applicationDbContext;
        private DbContextOptions<BlogDbContext> blog_options;
        private DbContextOptions<ApplicationDbContext> app_options;
        private IUserService userService;

        [TestInitialize]
        public void RunBeforeEachTest()
        {
            blog_options = new DbContextOptionsBuilder<BlogDbContext>()
                            .UseInMemoryDatabase(databaseName: "BlogDb")
                            .Options;

            app_options = new DbContextOptionsBuilder<ApplicationDbContext>()
                            .UseInMemoryDatabase(databaseName: "BlogDb")
                            .Options;

            var adult = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "adult",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var young = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "young",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var elder = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "elder",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            applicationDbContext = new ApplicationDbContext(app_options);
            applicationDbContext.Users.RemoveRange(applicationDbContext.Users);
            applicationDbContext.Users.Add(adult);
            applicationDbContext.Users.Add(young);
            applicationDbContext.Users.Add(elder);
            applicationDbContext.SaveChanges();

            blogDbContext = new BlogDbContext(blog_options);
            blogDbContext.Users.RemoveRange(blogDbContext.Users);
            blogDbContext.Users.Add(new User { Id = 1, AgeGroup = AgeGroup.ADULT, ApplicationUserId = adult.Id, FirstName = "adult_firstname", LastName = "adult_lastname", Username = adult.UserName });
            blogDbContext.Users.Add(new User { Id = 2, AgeGroup = AgeGroup.YOUNG, ApplicationUserId = young.Id, FirstName = "young_firstname", LastName = "young_lastname", Username = young.UserName });
            blogDbContext.Users.Add(new User { Id = 3, AgeGroup = AgeGroup.ELDER, ApplicationUserId = elder.Id, FirstName = "elder_firstname", LastName = "elder_lastname", Username = elder.UserName });
            blogDbContext.SaveChanges();

            userService = new UserService(new AsyncRepository<User>(blogDbContext));
        }

        [TestMethod]
        public void GetAll()
        {
            IEnumerable<User> users = userService.GetAll().Result;

            Assert.AreEqual(3, users.Count());
        }

        [TestMethod]
        public void GetUserById()
        {
            User user = userService.GetUserById(1).Result;

            Assert.IsNotNull(user);
            Assert.AreEqual("adult", user.Username);
        }

        [TestMethod]
        public void WhoAmI()
        {
            IEnumerable<User> users = userService.GetAll().Result;
            User user = users.First();
            Assert.IsNotNull(user);

            User amI = userService.WhoAmI(user.ApplicationUserId).Result;

            Assert.IsNotNull(amI);
            Assert.AreEqual(user.ApplicationUserId, amI.ApplicationUserId);
        }

        [TestMethod]
        public void SignUp()
        {
            User user = userService.SignUp("firstname", "lastname", "ADULT", "test", Guid.NewGuid().ToString()).Result;

            Assert.IsNotNull(user);
            Assert.IsNotNull(user.Id);
            Assert.AreEqual("test", user.Username);
        }
    }
}
