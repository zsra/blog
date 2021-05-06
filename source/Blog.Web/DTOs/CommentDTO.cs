using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.Web.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Body is required")]
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required(ErrorMessage = "Author is required")]
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }

        public ICollection<CommentDTO> Comments { get; set; }
    }
}
