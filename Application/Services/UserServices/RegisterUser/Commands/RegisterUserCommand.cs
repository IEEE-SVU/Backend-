using Domain.IRepositories;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserServices.RegisterUser.Commands
{
    public record RegisterUserCommand(string Username, string Email, string Password) : IRequest<bool>;

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
                Email = request.Email,
                PasswordHash = request.Password 
            };
            await _userRepo.AddAsync(user);
            await _userRepo.SaveChangesAsync();
            return true;
        }
    }
}
