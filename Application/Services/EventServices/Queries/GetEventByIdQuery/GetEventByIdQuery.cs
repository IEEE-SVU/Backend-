using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Helper;
using Application.Enums;
using Application.Services.EventServices.DTOs;
using Domain.IRepositories;
using Domain.Models;
using MediatR;

namespace Application.Services.EventServices.Queries.GetEventByIdQuery;
public record GetEventByIdQuery(Guid Id)
    : IRequest<RequestResult<EventDto>>;

public class GetEventByIdHandler(IRepository<Event> repository)
    : IRequestHandler<GetEventByIdQuery, RequestResult<EventDto>>
{

    private readonly IRepository<Event> _repository = repository;


    public async Task<RequestResult<EventDto>> Handle(
        GetEventByIdQuery request,
        CancellationToken cancellationToken)
    {
        var eventExist = await _repository.GetByIDAsync(request.Id);

        if (eventExist is null || eventExist!.IsDeleted == true)
            return RequestResult<EventDto>.Failure(ErrorCode.NotFound, "Event not found");

        var dto = new EventDto
        {
            Id = eventExist.Id,
            Name = eventExist.Name,
            Title = eventExist.Title,
            Category = eventExist.Category,
            Location = eventExist.Location,
            Date = eventExist.Date,
            ImageUrl = eventExist.ImageUrl
        };

        return RequestResult<EventDto>.Success(dto);
    }
}
