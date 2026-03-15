using Domain.Enums;

namespace Application.Services.MembershipServices.DTOs
{
    public class MembershipDto
    {
        public ApplicationStatus Status { get; set; }
        public InterviewDetailsDto? Interview { get; set; }
    }

    
}
