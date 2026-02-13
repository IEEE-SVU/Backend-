using Domain.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Application.Common.PasswordHasher;
using Application.Common.TokenGenerator;

namespace Application.Services.UserServices.LoginUser.Commands
{
    public record LoginUserCommand(string Email, string Password) : IRequest<string>;
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
    {
        private IRepository<User> _repository;
        private IPasswordHasher _passwordHasher;
        private ITokenGenerator _tokenGenerator;
        public LoginUserCommandHandler(IRepository<User> repository , IPasswordHasher passwordHasher ,
            ITokenGenerator tokenGenerator)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
        }
        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.Get(x => x.Email == request.Email).FirstOrDefaultAsync();
            if (user == null)
            {
                return string.Empty;
            }

           if(!_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
            {
                return string.Empty;
            }


           return await _tokenGenerator.GenerateTokenAsync(user.Id);
            throw new NotImplementedException();
        }
    }
}