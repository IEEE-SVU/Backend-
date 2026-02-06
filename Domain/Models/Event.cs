using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{

    public class Event : BaseModel
    {
        public string Name { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty; 
        public string Location { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public EventType Type { get; set; }
        public Guid CommunityId { get; set; }
        #region Navigation Properties
        public virtual Community Community { get; set; }
        #endregion
    }
}
