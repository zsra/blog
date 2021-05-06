using Blog.Core.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.Web.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Author is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Author is required")]
        public AgeGroup[] AgeGroups { get; set; }
    }
}
