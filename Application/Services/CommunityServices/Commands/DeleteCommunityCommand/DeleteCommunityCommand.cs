using Domain.IRepositories;
using Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.CommunityServices.Commands.DeleteCommunityCommand
{
    public record DeleteCommunityCommand(Guid Id) : IRequest<bool>;

    public class DeleteCommunityHandler(IRepository<Community> repository) : IRequestHandler<DeleteCommunityCommand, bool>
    {

        private readonly IRepository<Community> _repository = repository;

        public async Task<bool> Handle(DeleteCommunityCommand request, CancellationToken cancellationToken)
        {
            var community = await _repository.GetByIDAsync(request.Id);
            if (community == null) return false;

            await _repository.DeleteAsync(community);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
