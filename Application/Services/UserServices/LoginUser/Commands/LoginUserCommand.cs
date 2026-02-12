using Domain.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.UserServices.LoginUser.Commands
{
    public record LoginUserCommand(string Email, string Password) : IRequest<string>;
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
    {
        private IRepository<User> _repository;
        public LoginUserCommandHandler(IRepository<User> repository)
        {
            _repository = repository;
        }
        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.Get(x => x.Email == request.Email).FirstOrDefaultAsync();
            if (user == null)
            {
                return string.Empty;
            }

            if (user.PasswordHash != request.Password)
            {
                return string.Empty;
            }


            throw new NotImplementedException();
        }
    }
}