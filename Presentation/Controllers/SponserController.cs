using Application.DTOs.SponserDTOs;
using Application.Services.SponserServices.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModels.Response;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SponserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SponserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<EndpointResponse<SponsorsPageResponseDto>> GetSponsorsPage()
        {
            var ans = await _mediator.Send(new GetSponsorsQuery());

            if (ans == null || ans.IsSuccess == false)
            {
                return EndpointResponse<SponsorsPageResponseDto>.Fail(ans.ErrorCode,ans.Message);
            }

            return EndpointResponse<SponsorsPageResponseDto>.Success(ans.Data);
        }
    }
}
