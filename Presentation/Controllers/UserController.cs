 using Application.DTOs.UserDTOs;
using Application.Services.UserServices.Certificates.DTOs;
using Application.Services.UserServices.EditeProfile.Commands;
using Application.Services.UserServices.GetCertificates.Queries;
using Application.Services.UserServices.GetProfile.Commands;
using Application.Services.UserServices.LoginUser.Commands;
using Application.Services.UserServices.RegisterUser.Commands;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("Register")]
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
            var answ = await _mediator.Send(new RegisterUserCommand
                (request.FullName,request.Email, request.University, request.FacultyOrDepartment, request.Password));
            if (answ.IsSuccess == false)
            {
                return EndpointResponse<bool>.Fail(answ.ErrorCode);
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
            var answ = await _mediator.Send(new LoginUserCommand(request.Email,request.Password));
            if (answ.IsSuccess == false)
            {
                return EndpointResponse<string>.Fail(answ.ErrorCode);
            }
            return EndpointResponse<String>.Success(answ.Data);
        }
        [Authorize]
        [HttpGet("Profile")]
        public async Task<EndpointResponse<UserProfileDTO>> GetProfile()
        {
            var answ = await _mediator.Send(new GetProfileCommand());

            if(answ.IsSuccess ==  false)
            {
                return EndpointResponse<UserProfileDTO>.Fail(answ.ErrorCode, answ.Message);
            }
            
            return EndpointResponse<UserProfileDTO>.Success(answ.Data);
        }
        [HttpPut("editprofile")]
        [Authorize]
        public async Task<EndpointResponse<bool>> EditProfile([FromForm] EditProfileViewModel request)
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

            var result = await _mediator.Send(new EditProfileCommand(
                request.FullName, request.PhoneNumber, request.University, request.Faculty,
                request.Image, request.DeleteImage, request.CV, request.DeleteCV
            ));
            
            if(result.IsSuccess == false)
            {
                return EndpointResponse<bool>.Fail(result.ErrorCode, result.Message);
            }

            return EndpointResponse<bool>.Success(result.Data);
        }
        [Authorize]
        [HttpGet("certificates")]
        public async Task<EndpointResponse<IEnumerable<CertificateDto>>> GetCertificates()
        {
            var result = await _mediator.Send(new GetUserCertificatesQuery());

            return result.IsSuccess ? EndpointResponse<IEnumerable<CertificateDto>>.Success(result.Data)
                : EndpointResponse<IEnumerable<CertificateDto>>.Fail(result.ErrorCode, result.Message);
        }
    }
}
