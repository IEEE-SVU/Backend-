using Application.Services.MembershipServices.DTOs;
using Application.Services.MembershipServices.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModels.Response;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MembershipController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MembershipController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<EndpointResponse<MembershipDto>> GetMembershipPage()
        {
            var result = await _mediator.Send(new GetMembershipQuery());

            return result.IsSuccess
                ? EndpointResponse<MembershipDto>.Success(result.Data)
                : EndpointResponse<MembershipDto>.Fail(result.ErrorCode, result.Message);
        }
    }
}