using System;

namespace Application.Services.CommunityServices.DTOs
{
    public class CommunityByIdDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public int MembersCount { get; set; }
        public int EventsCount { get; set; }
        public List<string> MainTasks { get; set; } = [];
        public List<string> Achievements { get; set; } = [];
        public bool IsJoiningOpen { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
