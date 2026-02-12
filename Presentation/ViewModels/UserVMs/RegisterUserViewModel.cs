using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels.UserVMs
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
        public string? PhoneNumber { get; set; } = null;
        public string? NationalId { get; set; } = null; 
        public string? Faculty { get; set; } = null;
        public string? Major { get; set; } = null;
        public IFormFile? CV { get; set; } = null;
    }
}