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
        public string Email { get; set; } = string.Empty;
        public string Universtiry { get; set; } = string.Empty;   
        public string FacultyOrDepartment { get; set; } = string.Empty; 
        public string PasswordHash { get; set; } = string.Empty;
        public bool IsVerfied { get; set; } = true;
        public Role Role { get; set; }
        public Guid? CommunityId { get; set; }
        #region Navigation Properties
        public virtual Community? Community { get; set; }
        public virtual ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();
        #endregion
    }
}
