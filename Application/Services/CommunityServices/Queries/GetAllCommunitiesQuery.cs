using Application.Services.CommunityServices.DTOs;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.CommunityServices.Queries
{
    public record GetAllCommunitiesQuery : IRequest<List<CommunityDto>>;

    public class GetAllCommunitiesHandler : IRequestHandler<GetAllCommunitiesQuery, List<CommunityDto>>
    {
        private readonly IRepository<Community> _repository;

        public GetAllCommunitiesHandler(IRepository<Community> repository)
        {
            _repository = repository;
        }

        public async Task<List<CommunityDto>> Handle(GetAllCommunitiesQuery request, CancellationToken cancellationToken)
        {
            var communities = await _repository.GetAll().ToListAsync(cancellationToken);

            return communities.Select(c => new CommunityDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                ImageUrl = c.ImageUrl,
                CreatedAt = c.CreatedAt
            }).ToList();
        }
    }
}
