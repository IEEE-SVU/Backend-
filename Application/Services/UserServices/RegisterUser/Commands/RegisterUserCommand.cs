using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Services.UserServices.RegisterUser.Commands
{
    public record RegisterUserCommand(string Username,string FullName, string Password
        , string? Email, string? PhoneNumber , string NationalId,string? Faculty, string? Major , IFormFile CV) : IRequest<bool>;

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, bool>
    {
        private readonly IRepository<User> _userRepo;
        public RegisterUserCommandHandler(IRepository<User> userRepo)
        {
            _userRepo = userRepo;
        }
        public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Username = request.Username,
                FullName = request.FullName,
                PasswordHash = request.Password,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                NationalId = request.NationalId,
                Faculty = request.Faculty,
                Major = request.Major,
               // CV = request.CV
            };
            await _userRepo.AddAsync(user);
            await _userRepo.SaveChangesAsync();
            return true;
        }
    }
}
