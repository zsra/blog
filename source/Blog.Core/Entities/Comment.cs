using System;
using System.Collections.Generic;

namespace Blog.Core.Entities
{
    public class Comment : Entity
    {
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public User Author { get; set; }
        public Post Post { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
