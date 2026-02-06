using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.CommunityServices.DTOs;
using Domain.IRepositories;
using Domain.Models;
using MediatR;

namespace Application.Services.CommunityServices.Queries;

public record GetCommunityByIdRequest(Guid Id) : IRequest<CommunityDto>;
public class GetCommunityById(IRepository<Community> repository) : IRequestHandler<GetCommunityByIdRequest, CommunityDto>
{
    IRepository<Community> _repository = repository;
    public Task<CommunityDto> Handle(GetCommunityByIdRequest request, CancellationToken cancellationToken)
    {
        var result = _repository.GetByIDAsync(request.Id);
        throw new Exception(); 
    }
}
