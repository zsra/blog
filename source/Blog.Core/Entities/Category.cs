using Blog.Core.Extensions;
using System.Collections.Generic;

namespace Blog.Core.Entities
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public string SerializedAgeGroups { get; set; }

        public ICollection<Post> Posts { get; set; }

        public IEnumerable<AgeGroup> GetAgeGroups() => SerializedAgeGroups.FromJson<IEnumerable<AgeGroup>>();
    }
}
