using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels.UserVMs
{
    public class EditProfileViewModel
    {
        [Required]
        public string FullName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string University { get; set; } = string.Empty;
        public string Faculty { get; set; } = string.Empty;
        public IFormFile? Image { get; set; }
        public IFormFile? CV { get; set; }
        public bool DeleteImage { get; set; }
        public bool DeleteCV { get; set; }
    }
}
