using Application.Services.CommunityServices.DTOs;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.CommunityServices.Commands.CreateCommunityCommand
{
    public record CreateCommunityCommand(CreateCommunityDto Dto) : IRequest<Guid>;

    public class CreateCommunityHandler(IRepository<Community> repository) : IRequestHandler<CreateCommunityCommand, Guid>
    {
        private readonly IRepository<Community> _repository = repository;

        public async Task<Guid> Handle(CreateCommunityCommand request, CancellationToken cancellationToken)
        {
            var community = new Community
            {
                Name = request.Dto.Name,
                Description = request.Dto.Description,
                ImageUrl = request.Dto.ImageUrl,
                IsJoiningOpen = request.Dto.IsJoiningOpen
            };

            await _repository.AddAsync(community);
            await _repository.SaveChangesAsync();

            return community.Id;
        }
    }
}
