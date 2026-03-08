using Application.Common.Helper;
using Application.Common.PasswordHasher;
using Application.Common.TokenGenerator;
using Application.Enums;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.UserServices.LoginUser.Commands
{
    public record LoginUserCommand(string Email, string Password) : IRequest<RequestResult<string>>;
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, RequestResult<string>>
    {
        private IRepository<User> _repository;
        private IPasswordHasher _passwordHasher;
        private ITokenGenerator _tokenGenerator;
        public LoginUserCommandHandler(IRepository<User> repository, IPasswordHasher passwordHasher,
            ITokenGenerator tokenGenerator)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
        }
        public async Task<RequestResult<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.Get(x => x.Email == request.Email).FirstOrDefaultAsync();
            if (user == null)
            {
                return RequestResult<string>.Failure(ErrorCode.NotFound, "User not found");
            }

            if (!_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
            {
                return RequestResult<string>.Failure(ErrorCode.Unauthorized, "Invalid password");
            }


            var token = await _tokenGenerator.GenerateTokenAsync(user.Id);

            return RequestResult<string>.Success(token, "Login successful");
        }
    }
}