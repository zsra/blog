using System.ComponentModel.DataAnnotations;

namespace Blog.Web.DTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Age group is required")]
        public string AgeGroup { get; set; }
    }
}
