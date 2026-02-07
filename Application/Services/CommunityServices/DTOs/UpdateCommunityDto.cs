using System.ComponentModel.DataAnnotations;

namespace Application.Services.CommunityServices.DTOs
{
    public class UpdateCommunityDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public bool IsJoiningOpen { get; set; }
    }
}
