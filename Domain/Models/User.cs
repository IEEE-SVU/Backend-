using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Domain.Models
{
    public class User : BaseModel
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string FullName { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string NationalId { get; set; } = string.Empty;  
        public string Faculty { get; set; } = string.Empty; 
        public string Major { get; set; } = string.Empty;   
        //public  IFormFile CV { get; set; }
        public bool IsVerfied { get; set; } = false;
        public Role Role { get; set; }
        public Guid? CommunityId { get; set; }
        #region Navigation Properties
        public virtual Community? Community { get; set; }
        #endregion
    }
}
