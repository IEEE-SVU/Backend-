using Application.Common.Helper;
using Application.Common.PasswordHasher;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Services.UserServices.RegisterUser.Commands
{
    public record RegisterUserCommand(string FullName, string? Email
       ,string? University, string? FacultyOrDepartment, string Password) : IRequest<RequestResult<bool>>;

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RequestResult<bool>>
    {
        private readonly IRepository<User> _userRepo;
        private IPasswordHasher _passwordHasher;
        public RegisterUserCommandHandler(IRepository<User> userRepo,
            IPasswordHasher passwordHasher)
        {
            _userRepo = userRepo;
            _passwordHasher = passwordHasher;
        }
        public async Task<RequestResult<bool>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                Universtiry = request.University,   
                FacultyOrDepartment = request.FacultyOrDepartment,
                PasswordHash = _passwordHasher.HashPassword(request.Password),
            };
            await _userRepo.AddAsync(user);
            await _userRepo.SaveChangesAsync();
            
            return RequestResult<bool>.Success(true, "User registered successfully");
        }
    }
}
