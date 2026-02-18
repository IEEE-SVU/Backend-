using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.CommunityServices.DTOs;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.CommunityServices.Queries.GetAllCommunitiesQuery;
public record GetAllCommunitiesQuery : IRequest<List<CommunityDto>>;

public class GetAllCommunitiesHandler(IRepository<Community> repository) : IRequestHandler<GetAllCommunitiesQuery, List<CommunityDto>>
{

    private readonly IRepository<Community> _repository = repository;

    public async Task<List<CommunityDto>> Handle(GetAllCommunitiesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll()
            .AsNoTracking()
            .Select(x => new CommunityDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                IsJoiningOpen = x.IsJoiningOpen
            }).ToListAsync(cancellationToken);
    }
}
