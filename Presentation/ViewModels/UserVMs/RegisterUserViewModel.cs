using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels.UserVMs
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
        [Required(ErrorMessage ="University is required")]
        public string University { get; set; }
        public string? FacultyOrDepartment { get; set; } = null;
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string ConfirmPassword { get; set; }

    }
}