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
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public string? Location { get; set; }
        public string? ImageUrl { get; set; }
        public EventType Type { get; set; }
        public Guid CommunityId { get; set; }
        #region Navigation Properties
        public virtual Community Community { get; set; }
        #endregion
    }
}
