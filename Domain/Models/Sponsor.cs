using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Sponsor : BaseModel
    {
        public string Name { get; set; } = string.Empty; 
        public string LogoUrl { get; set; } = string.Empty; 
        public string? WebsiteUrl { get; set; }
        public SponsorTier Tier { get; set; }
    }
}
