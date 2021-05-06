using Blog.Core.Entities;
using Blog.Core.Extensions;
using Blog.Web.Converters;
using Blog.Web.DTOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Blog.Test.Converters
{
    [TestClass]
    public class UserConvertersTest
    {
        [TestMethod]
        public void EntityToDto()
        {
            User user = new()
            {
                AgeGroup = AgeGroup.ADULT,
                ApplicationUserId = Guid.NewGuid().ToString(),
                FirstName = "firstname",
                LastName = "lastname",
                Id = 1,
                Posts = new List<Post> {
                    new Post
                    {
                        Id = 1,
                        Body = "Lorum ipsum",
                        Categories = new List<Category>
                        {
                            new Category
                            {
                                Id = 1,
                                Name = "Sport",
                                SerializedAgeGroups = (new AgeGroup[] { AgeGroup.ADULT, AgeGroup.ELDER }).ToJson()
                            }
                        }
                    }
                },
                Username = "username"
            };

            UserDTO userDTO = user.EntityToDto();

            Assert.IsNotNull(userDTO);
            Assert.AreEqual(AgeGroup.ADULT.ToString(), userDTO.AgeGroup);
            Assert.AreEqual(user.FirstName, userDTO.FirstName);
            Assert.AreEqual(user.LastName, userDTO.LastName);
            Assert.AreEqual(user.Id, userDTO.Id);
            Assert.AreEqual(user.Username, userDTO.Username);
        }

        [TestMethod]
        public void DtoToEntity()
        {
            List<Post> posts = new()
            {
                new Post
                {
                    Author = new User { Id = 1 }
                },
                new Post
                {
                    Author = new User { Id = 2 }
                },
                new Post
                {
                    Author = new User { Id = 1 }
                },
            };

            UserDTO userDTO = new()
            {
                Username = "username",
                LastName = "lastname",
                FirstName = "firstname",
                AgeGroup = "ADULT",
                Id = 1
            };

            User user = userDTO.DtoToEntity(posts);

            Assert.IsNotNull(user);
            Assert.AreEqual(userDTO.Username, user.Username);
            Assert.AreEqual(userDTO.FirstName, user.FirstName);
            Assert.AreEqual(userDTO.LastName, user.LastName);
            Assert.AreEqual(userDTO.AgeGroup, user.AgeGroup.ToString());
            Assert.AreEqual(2, user.Posts.Count);
        }
    }
}
