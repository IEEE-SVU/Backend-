using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Community : BaseModel
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        #region Navigation Properties
        public virtual ICollection<User> Users { get; set; }    
        public virtual ICollection<Event> Events { get; set; }
        #endregion
    }
}
