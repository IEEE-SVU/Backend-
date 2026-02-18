using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.IRepositories;
using Domain.Models;
using MediatR;

namespace Application.Services.CommunityServices.Commands.OpenIsJoinCommunityCommand;
public record OpenIsJoinCommunityCommand(Guid Id) : IRequest<bool>;
public class OpenIsJoinCommunityHandler(IRepository<Community> repository) : IRequestHandler<OpenIsJoinCommunityCommand, bool>
{
    private readonly IRepository<Community> _repository = repository;

    public async Task<bool> Handle(OpenIsJoinCommunityCommand request, CancellationToken cancellationToken)
    {
        var community = await _repository.GetByIDAsync(request.Id);

        if (community is null)
            return false;

        community.IsJoiningOpen = !community.IsJoiningOpen;

        await _repository.SaveIncludeAsync(
             community,
             nameof(Community.IsJoiningOpen)
        );

        await _repository.SaveChangesAsync();

        return true;
    }
}
