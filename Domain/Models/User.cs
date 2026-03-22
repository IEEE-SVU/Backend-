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
        public string FullName { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Universtiry { get; set; } = string.Empty;   
        public string FacultyOrDepartment { get; set; } = string.Empty; 
        public string PasswordHash { get; set; } = string.Empty;
        public string PhoneNumber { get; set;} = string.Empty;
        public string CV { get; set; } = string.Empty;
        public bool IsVerfied { get; set; } = true;
        public UserStatus Status { get; set; }
        public Role? Role { get; set; }
        public Guid? CommunityId { get; set; }

        #region Navigation Properties
        public virtual Community? Community { get; set; }
        public virtual ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();
        public virtual ICollection<UserEvent> UserEvents { get; set; }

        #endregion
    }
}
