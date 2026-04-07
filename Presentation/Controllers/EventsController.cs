using Application.Events;
using Application.Services.EventServices.DTOs;
using Application.Services.EventServices.Queries.GetEventByIdQuery;
using Application.Services.EventServices.Queries.GetLatestEventsQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModels.Response;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EventsController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetEvents([FromQuery] string? category)
        {
            var events = await _mediator.Send(new GetAllEventsQuery(category));
            return Ok(events);
        }
        [HttpGet("latest")]
        public async Task<EndpointResponse<IEnumerable<EventDto>>> GetLatest([FromQuery] int count = 10)
        {
            var result = await _mediator.Send(new GetLatestEventsQuery(count));

            return EndpointResponse<IEnumerable<EventDto>>.Success(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<EndpointResponse<EventDto>> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetEventByIdQuery(id));

            if (!result.IsSuccess)
                return EndpointResponse<EventDto>.Fail(result.ErrorCode, result.Message);

            return EndpointResponse<EventDto>.Success(result.Data!);
        }
    }
}
