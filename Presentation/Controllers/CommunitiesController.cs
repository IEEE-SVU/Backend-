using Application.Services.CommunityServices.DTOs;
using Application.Services.CommunityServices.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunitiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommunitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<CommunityDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllCommunitiesQuery());
            return Ok(result);
        }
    }
}
