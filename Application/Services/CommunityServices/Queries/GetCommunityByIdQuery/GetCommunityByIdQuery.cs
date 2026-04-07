using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Helper;
using Application.Services.CommunityServices.DTOs;
using Domain.IRepositories;
using Domain.Models;
using MediatR;

namespace Application.Services.CommunityServices.Queries.GetCommunityByIdQuery
{
    public record GetCommunityByIdQuery(Guid Id) : IRequest<RequestResult<CommunityByIdDto?>>;

    public class GetCommunityByIdHandler(IRepository<Community> repository) : IRequestHandler<GetCommunityByIdQuery, RequestResult<CommunityByIdDto?>>
    {

        private readonly IRepository<Community> _repository = repository;

        public async Task<RequestResult<CommunityByIdDto?>> Handle(GetCommunityByIdQuery request, CancellationToken cancellationToken)
        {
            var community = await _repository.GetByIDAsync(request.Id);

            if (community is null || community.IsDeleted) 
                return RequestResult<CommunityByIdDto?>.Failure(Enums.ErrorCode.BadRequest,"this Community not found");

            var communityById = new CommunityByIdDto
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

            return RequestResult<CommunityByIdDto?>.Success(communityById);
        }
    }
}
