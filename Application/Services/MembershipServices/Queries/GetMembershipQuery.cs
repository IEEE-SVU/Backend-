using Application.Common.Helper;
using Application.Common.UserInfo;
using Application.Enums;
using Application.Services.MembershipServices.DTOs;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.MembershipServices.Queries
{
    public record GetMembershipQuery() : IRequest<RequestResult<MembershipDto>>;

    public class GetMembershipQueryHandler : IRequestHandler<GetMembershipQuery, RequestResult<MembershipDto>>
    {
        private readonly IRepository<MembershipApplication> _appRepo;
        private readonly IRepository<User> _userRepo;
        private readonly ICurrentUserService _currentUser;

        public GetMembershipQueryHandler(
            IRepository<MembershipApplication> appRepo,
            IRepository<User> userRepo,
            ICurrentUserService currentUser)
        {
            _appRepo = appRepo;
            _userRepo = userRepo;
            _currentUser = currentUser;

        }

        public async Task<RequestResult<MembershipDto>> Handle(GetMembershipQuery request, CancellationToken cancellationToken)
        {
            if (!_currentUser.IsAuthenticated || _currentUser.Id is null)
                return RequestResult<MembershipDto>.Failure(ErrorCode.Unauthorized, "You must be logged in.");

            var user = await _userRepo.GetByIDAsync(_currentUser.Id.Value);
            if (user is null || user.CommunityId is null)
                return RequestResult<MembershipDto>.Failure(ErrorCode.NotFound, "You are not associated with any community.");
            var application = await _appRepo
                .Get(a => a.UserId == _currentUser.Id.Value && a.CommunityId == user.CommunityId.Value)
                .Include(a => a.Interview)
                .FirstOrDefaultAsync(cancellationToken);

            if (application is null)
                return RequestResult<MembershipDto>.Failure(ErrorCode.NotFound, "No application found.");

            var dto = new MembershipDto
            {
                Status = application.Status,
                Interview = application.Interview is null ? null : new InterviewDetailsDto
                {
                    Date = application.Interview.Date,
                    MeetingLink = application.Interview.MeetingLink,
                    IsOnline = application.Interview.IsOnline
                }
            };

            return RequestResult<MembershipDto>.Success(dto);
        }
    }
}
