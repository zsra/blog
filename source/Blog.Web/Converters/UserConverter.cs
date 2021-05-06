using Blog.Core.Entities;
using Blog.Core.Extensions;
using Blog.Web.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Web.Converters
{
    public static class UserConverter
    {
        public static UserDTO EntityToDto(this User user) => new()
        {
            Id = user.Id,
            AgeGroup = user.AgeGroup.ToString(),
            FirstName = user.FirstName,
            LastName = user.LastName,
            Username = user.Username
        };

        public static User DtoToEntity(this UserDTO userDTO, IEnumerable<Post> posts)
        {
            if (!posts.Any()) return null;

            _ = Enum.TryParse(userDTO.AgeGroup, out AgeGroup ageGroup);

            return new User
            {
                Id = userDTO.Id,
                AgeGroup = ageGroup,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Username = userDTO.Username,
                Posts = new List<Post>(posts.Where( post => post.Author.Id == userDTO.Id ))
            };
        }
    }
}
