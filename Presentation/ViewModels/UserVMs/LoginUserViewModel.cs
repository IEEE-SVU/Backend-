using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels.UserVMs
{
    public class LoginUserViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Enter a valide Email.")]
        public string Email { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }
    }
}
