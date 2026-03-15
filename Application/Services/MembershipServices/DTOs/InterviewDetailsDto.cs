using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.MembershipServices.DTOs
{
    public class InterviewDetailsDto
    {
        public DateTime Date { get; set; }
        public string MeetingLink { get; set; } = string.Empty;
        public bool IsOnline { get; set; }
    }
}
