using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModels.Response;
using Presentation.ViewModels.UserVMs;
using System.Runtime.CompilerServices;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        //private readonly IValidator<T> validator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<EndpointResponse<bool>> RegisterUser([FromForm] RegisterUserViewModel request)
        {
            if (request == null)
            {
                return EndpointResponse<bool>.Fail(Application.Enums.ErrorCode.BadRequest);
            }
            if (!ModelState.IsValid)
            {
                var errors = string.Join(", ", ModelState.Values
                                                .SelectMany(v => v.Errors)
                                                .Select(e => e.ErrorMessage));

                return EndpointResponse<bool>.Fail(Application.Enums.ErrorCode.ValidationError, errors);
            }
            var answ = await _mediator.Send(new Application.Services.UserServices.RegisterUser.Commands.RegisterUserCommand
                (request.Username,request.FullName,request.Password,request.Email,request.PhoneNumber,request.NationalId,request.Faculty,request.Major,request.CV
                ));
            if (answ == null)
            {
                return EndpointResponse<bool>.Fail(Application.Enums.ErrorCode.BadRequest);
            }
            return EndpointResponse<bool>.Success(true);
        }
        [HttpPost("login")]
        public async Task<EndpointResponse<string>> Login([FromBody] LoginUserViewModel request)
        {
            if (request == null)
            {
                return EndpointResponse<string>.Fail(Application.Enums.ErrorCode.BadRequest);
            }
            if (!ModelState.IsValid)
            {
                var errors = string.Join(", ", ModelState.Values
                                                .SelectMany(v => v.Errors)
                                                .Select(e => e.ErrorMessage));

                return EndpointResponse<string>.Fail(Application.Enums.ErrorCode.ValidationError, errors);
            }
            var answ = await _mediator.Send(new Application.Services.UserServices.LoginUser.Commands.LoginUserCommand(request.Email,request.Password));
            if (answ == null)
            {
                return EndpointResponse<string>.Fail(Application.Enums.ErrorCode.BadRequest);
            }
            return EndpointResponse<String>.Success(answ);
        }

    }
}
