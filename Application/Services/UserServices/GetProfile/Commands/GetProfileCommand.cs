using Application.Common.Helper;
using Application.Common.UserInfo;
using Application.DTOs.UserDTOs;
using Application.Enums;
using Domain.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserServices.GetProfile.Commands
{
    public record GetProfileCommand() : IRequest<RequestResult<UserProfileDTO>>;
    public class GetProfileHandler : IRequestHandler<GetProfileCommand, RequestResult<UserProfileDTO>>
    {
        public ICurrentUserService _currentUser;
        public IRepository<Domain.Models.User> _UserRepo;
        public GetProfileHandler(ICurrentUserService currentUser , IRepository<Domain.Models.User> repository)
        {
            _currentUser = currentUser;
            _UserRepo = repository;
        }
        public async Task<RequestResult<UserProfileDTO>> Handle(GetProfileCommand request, CancellationToken cancellationToken)
        {
            if (_currentUser.IsAuthenticated == false)
            {
                return RequestResult<UserProfileDTO>.Failure(ErrorCode.Unauthorized, "User is not authenticated.");

            }
            
            var user = await _UserRepo.GetByIDAsync(_currentUser.Id??Guid.Empty);

            var userProfile = new UserProfileDTO
            {
                FullName = user.FullName,
                ImageUrl = user.ImageUrl,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                University = user.Universtiry,
                Faculty = user.FacultyOrDepartment,
                CV = user.CV
            };

            return RequestResult<UserProfileDTO>.Success(userProfile);
        }
    }
}
