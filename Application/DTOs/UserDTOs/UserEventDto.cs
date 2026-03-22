using Domain.Enums;
using System;

namespace  Application.DTOs.UserDTOs
{
    public class UserEventDTO 
    {
        public Guid EventId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public EventType EventType { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public DateTime RegisteredAt { get; set; }
    }
}