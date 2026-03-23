using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.SponserDTOs
{
    public class SponsorsPageResponseDto
    {
        public List<SponsorDto> AcademicAffiliations { get; set; } = new();
        public List<SponsorDto> PlatinumSponsors { get; set; } = new();
        public List<SponsorDto> GoldSponsors { get; set; } = new();
        public List<SponsorDto> SilverSponsors { get; set; } = new();
    }
    public class SponsorDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
        public string? WebsiteUrl { get; set; }
    }
}
