using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Helper;
using Application.Services.EventServices.DTOs;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.EventServices.Queries.GetLatestEventsQuery;
public record GetLatestEventsQuery(int Count = 10)
    : IRequest<RequestResult<IEnumerable<EventDto>>>;

public class GetLatestEventsHandler(IRepository<Event> repository)
    : IRequestHandler<GetLatestEventsQuery, RequestResult<IEnumerable<EventDto>>>
{
    private readonly IRepository<Event> _repository = repository;

    public async Task<RequestResult<IEnumerable<EventDto>>> Handle(GetLatestEventsQuery request, CancellationToken cancellationToken)
    {
        var latestEvents = await _repository
            .GetAll()
            .Where(e => !e.IsDeleted)
            .OrderByDescending(e => e.Date)
            .Take(request.Count)
            .Select(e => new EventDto
            {
                Id = e.Id,
                Name = e.Name,
                Title = e.Title,
                Category = e.Category,
                Location = e.Location,
                Date = e.Date,
                ImageUrl = e.ImageUrl
            })
            .ToListAsync(cancellationToken); 

        return RequestResult<IEnumerable<EventDto>>.Success(latestEvents);
    }
}