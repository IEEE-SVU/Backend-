using Application.Services.CommunityServices.DTOs;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.CommunityServices.Commands.UpdateCommunityCommand
{
    public record UpdateCommunityCommand(UpdateCommunityDto Dto) : IRequest<bool>;

    public class UpdateCommunityHandler(IRepository<Community> repository) : IRequestHandler<UpdateCommunityCommand, bool>
    {

        private readonly IRepository<Community> _repository  = repository;

        public async Task<bool> Handle(UpdateCommunityCommand request, CancellationToken cancellationToken)
        {
            var community = await _repository.GetByIDAsync(request.Dto.Id);
            if (community is null)
                return false;

            community.Name = request.Dto.Name;
            community.Description = request.Dto.Description;
            community.ImageUrl = request.Dto.ImageUrl;
            community.IsJoiningOpen = request.Dto.IsJoiningOpen;

            var updated = await _repository.SaveIncludeAsync(
                community,
                nameof(Community.Name),
                nameof(Community.Description),
                nameof(Community.ImageUrl),
                nameof(Community.IsJoiningOpen)
            );

            if (!updated)
                return false;

            await _repository.SaveChangesAsync();

            return true;
        }
    }
}
