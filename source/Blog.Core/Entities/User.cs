using Blog.Core.Extensions;
using System.Collections.Generic;

namespace Blog.Core.Entities
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public AgeGroup AgeGroup { get; set; }
        
        public string ApplicationUserId { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
