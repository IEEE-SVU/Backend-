using Application.Common.Helper;
using Application.Common.UserInfo;
using Application.DTOs.UserDTOs;
using Application.Enums;
using Domain.Models;
using Domain.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.UserServices.GetProfile.Commands
{
    public record GetProfileCommand() : IRequest<RequestResult<UserProfileDTO>>;

    public class GetProfileHandler : IRequestHandler<GetProfileCommand, RequestResult<UserProfileDTO>>
    {
        private readonly ICurrentUserService _currentUser;
        private readonly IRepository<User> _userRepo;
        private readonly IRepository<UserEvent> _userEventRepo; 

        public GetProfileHandler(
            ICurrentUserService currentUser,
            IRepository<User> userRepo,
            IRepository<UserEvent> userEventRepo) 
        {
            _currentUser = currentUser;
            _userRepo = userRepo;
            _userEventRepo = userEventRepo; 
        }

        public async Task<RequestResult<UserProfileDTO>> Handle(GetProfileCommand request, CancellationToken cancellationToken)
        {
            if (_currentUser.IsAuthenticated == false)
            {
                return RequestResult<UserProfileDTO>.Failure(
                    ErrorCode.Unauthorized,
                    "User is not authenticated."
                );
            }

            var userId = _currentUser.Id ?? Guid.Empty; 
            var user = await _userRepo.GetByIDAsync(userId);

            if (user == null) 
            {
                return RequestResult<UserProfileDTO>.Failure(
                    ErrorCode.NotFound,
                    "User not found."
                );
            }

            var userEvents = await _userEventRepo 
                .Get(ue => ue.UserId == userId)
                .Include(ue => ue.Event)
                .OrderByDescending(ue => ue.RegisteredAt)
                .ToListAsync(cancellationToken);

            var userProfile = new UserProfileDTO
            {
                FullName = user.FullName,
                ImageUrl = user.ImageUrl,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                University = user.Universtiry,
                Faculty = user.FacultyOrDepartment,
                CV = user.CV,
                Status = user.Status,
                RegisteredEvents = userEvents.Select(ue => new UserEventDTO
                {
                    EventId = ue.EventId,
                    Title = ue.Event.Title,
                    ImageUrl = ue.Event.ImageUrl,
                    EventType = ue.Event.Type,
                    EventDate = ue.Event.Date,
                    Location = ue.Event.Location,
                    RegisteredAt = ue.RegisteredAt
                }).ToList()
            };

            return RequestResult<UserProfileDTO>.Success(userProfile);
        }
    }
}