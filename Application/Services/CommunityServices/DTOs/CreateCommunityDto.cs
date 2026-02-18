using System.ComponentModel.DataAnnotations;

namespace Application.Services.CommunityServices.DTOs
{
    public class CreateCommunityDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public ICollection<string> Achievments { get; set; } = [];
        public ICollection<string> MainTasks { get; set; } = [];

        public bool IsJoiningOpen { get; set; }
    }
}
