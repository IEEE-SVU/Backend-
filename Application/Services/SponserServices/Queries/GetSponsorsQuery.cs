using Application.Common.Helper;
using Application.DTOs.SponserDTOs;
using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.SponserServices.Queries
{
    public record GetSponsorsQuery() : IRequest<RequestResult<SponsorsPageResponseDto>>;
    public class GetSponsorsHandler : IRequestHandler<GetSponsorsQuery, RequestResult<SponsorsPageResponseDto>>
    {
        private readonly IRepository<Sponsor> _sponsorRepo;

        public GetSponsorsHandler(IRepository<Sponsor> sponsorRepo)
        {
            _sponsorRepo = sponsorRepo;
        }

        public async Task<RequestResult<SponsorsPageResponseDto>> Handle(GetSponsorsQuery request, CancellationToken cancellationToken)
        {
            var allSponsors = await _sponsorRepo.GetAll().ToListAsync(cancellationToken);

            var response = new SponsorsPageResponseDto
            {
                AcademicAffiliations = allSponsors
                    .Where(s => s.Tier == SponsorTier.AcademicAffiliation)
                    .Select(s => MapToDto(s))
                    .ToList(),

                PlatinumSponsors = allSponsors
                    .Where(s => s.Tier == SponsorTier.Platinum)
                    .Select(s => MapToDto(s))
                    .ToList(),

                GoldSponsors = allSponsors
                    .Where(s => s.Tier == SponsorTier.Gold)
                    .Select(s => MapToDto(s))
                    .ToList(),

                SilverSponsors = allSponsors
                    .Where(s => s.Tier == SponsorTier.Silver)
                    .Select(s => MapToDto(s))
                    .ToList()
            };

            return RequestResult<SponsorsPageResponseDto>.Success(response);
        }
        private static SponsorDto MapToDto(Sponsor sponsor)
        {
            return new SponsorDto
            {
                Id = sponsor.Id,
                Name = sponsor.Name,
                LogoUrl = sponsor.LogoUrl,
                WebsiteUrl = sponsor.WebsiteUrl
            };
        }
    }
}
