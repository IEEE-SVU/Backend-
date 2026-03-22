using Domain.Models;
using System;

namespace Domain.Models
{
    public class UserEvent : BaseModel
    {
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
        public DateTime RegisteredAt { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual Event Event { get; set; }
    }
}