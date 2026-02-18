using Application.Services.CommunityServices.DTOs;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.CommunityServices.Queries.GetCommunityByIdQuery
{
    public record GetCommunityByIdQuery(Guid Id) : IRequest<CommunityByIdDto?>;

    public class GetCommunityByIdHandler(IRepository<Community> repository) : IRequestHandler<GetCommunityByIdQuery, CommunityByIdDto?>
    {

        private readonly IRepository<Community> _repository = repository;

        public async Task<CommunityByIdDto?> Handle(GetCommunityByIdQuery request, CancellationToken cancellationToken)
        {
            var community = await _repository.GetByIDAsync(request.Id);

            if (community is null) 
                return null;

            return new CommunityByIdDto
            {
                Id = community.Id,
                Name = community.Name,
                Description = community.Description,
                ImageUrl = community.ImageUrl,
                IsJoiningOpen = community.IsJoiningOpen,
                CreatedAt = community.CreatedAt,
                MembersCount = community.Users.Count,
                EventsCount = community.Events.Count,
                MainTasks = community.MainTasks.Select(t => t.Name).ToList(),
                Achievements = community.Achievments.Select(a => a.Name).ToList()
            };
        }
    }
}
