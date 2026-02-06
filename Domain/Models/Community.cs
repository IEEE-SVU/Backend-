using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Community : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsJoiningOpen { get; set; }

        #region Navigation Properties
        public virtual ICollection<User> Users { get; set; } = []; 
        public virtual ICollection<Event> Events { get; set; } = [];
        public virtual ICollection<MainTask> MainTasks { get; set; } = [];
        public virtual ICollection<Achievment> Achievments { get; set; } = [];

        #endregion
    }
}
