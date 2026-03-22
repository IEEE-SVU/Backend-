using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.UserDTOs;
namespace Application.DTOs.UserDTOs
{
    public class UserProfileDTO
    {
        public string FullName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;    
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string University { get; set; } = string.Empty;
        public string Faculty { get; set; } = string.Empty;
        public string CV { get; set; } = string.Empty;
        public UserStatus Status { get; set; }
        public List<UserEventDTO> RegisteredEvents { get; set; }

    }
}
