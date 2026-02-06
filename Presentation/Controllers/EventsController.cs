using Application.Events;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    { //o88
        private readonly IMediator _mediator;
        public EventsController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetEvents([FromQuery] string? category)
        {
            var events = await _mediator.Send(new GetAllEventsQuery(category));
            return Ok(events);
        }
    }
}
