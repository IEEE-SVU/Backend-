using Application.Services.CommunityServices.DTOs;
using Application.Services.CommunityServices.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Services.CommunityServices.Commands.CreateCommunityCommand;
using Application.Services.CommunityServices.Commands.UpdateCommunityCommand;
using Application.Services.CommunityServices.Commands.DeleteCommunityCommand;
using Application.Services.CommunityServices.Queries.GetCommunityByIdQuery;
using Application.Services.CommunityServices.Queries.GetAllCommunitiesQuery;
using Presentation.ViewModels.Response;
using Application.Services.CommunityServices.Commands.OpenIsJoinCommunityCommand;

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
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCommunitiesQuery());

            return Ok(EndpointResponse<List<CommunityDto>>.Success(result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetCommunityByIdQuery(id));
            if (result is null)
                return NotFound();

            return result is not null ? Ok(EndpointResponse<CommunityDto>.Success(result))
                : NotFound(EndpointResponse<CommunityDto>.Fail(Application.Enums.ErrorCode.NotFound,"This Community Not Found"));
        }

        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] CreateCommunityDto dto)
        {
            var communityId = await _mediator.Send(new CreateCommunityCommand(dto));

            return CreatedAtAction(nameof(GetById),
        new { id = communityId },
        EndpointResponse<Guid>.Success(communityId));
        }

        [HttpPut("")]
        public async Task<EndpointResponse<bool>> Update([FromBody] UpdateCommunityDto dto)
        {
            var result = await _mediator.Send(new UpdateCommunityCommand(dto));
            if (result == true)
                return EndpointResponse<bool>.Success(true);
            return EndpointResponse<bool>.Fail(Application.Enums.ErrorCode.BadRequest);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteCommunityCommand(id));
            if (!result)
                return NotFound(EndpointResponse<CommunityDto>.Fail(Application.Enums.ErrorCode.NotFound, "This Community Not Found"));
            return NoContent();
        }

        [HttpPut("{id}/openjoin")]
        public async Task<IActionResult> OpenIsJoin(Guid id)
        {
            var result = await _mediator.Send(new OpenIsJoinCommunityCommand(id));

            if(!result)
                return NotFound(EndpointResponse<CommunityDto>.Fail(Application.Enums.ErrorCode.NotFound, "This Community Not Found"));
            return NoContent();

        }
    }
}
