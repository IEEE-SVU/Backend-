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

            var existingAchievements = community.Achievments
                .Select(a => a.Name)
                .ToHashSet();

            foreach (var name in request.Dto.Achievments)
            {
                if (!existingAchievements.Contains(name))
                {
                    community.Achievments.Add(new Achievment
                    {
                        Name = name,
                        CommunityId = community.Id
                    });
                }
            }

            var existingTasks = community.MainTasks
                .Select(t => t.Name)
                .ToHashSet();

            foreach (var name in request.Dto.MainTasks)
            {
                if (!existingTasks.Contains(name))
                {
                    community.MainTasks.Add(new MainTask
                    {
                        Name = name,
                        CommunityId = community.Id
                    });
                }
            }

            await _repository.SaveChangesAsync();

            return true;
        }
    }
}
