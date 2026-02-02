using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost]
        public async Task<IActionResult> RegisterUser(string Username, string Email, string Password)
        {
            var answ = await _mediator.Send(new Application.Services.UserServices.RegisterUser.Commands.RegisterUserCommand(Username, Email, Password));
            return Ok(answ);
        }
    }
}
