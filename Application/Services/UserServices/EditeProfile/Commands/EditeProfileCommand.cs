using Application.Common.CloudinaryManagment;
using Application.Common.Helper;
using Application.Common.UserInfo;
using Application.Enums;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserServices.EditeProfile.Commands
{
    public record EditProfileCommand(
        string FullName,
        string? PhoneNumber,
        string University,
        string Faculty,
        IFormFile? Image,
        bool DeleteImage,
        IFormFile? CV,
        bool DeleteCV
    ) : IRequest<RequestResult<bool>>;


    public class EditProfileHandler : IRequestHandler<EditProfileCommand, RequestResult<bool>>
    {
        private readonly ICurrentUserService _currentUser;
        private readonly IRepository<User> _userRepo;
        private readonly ICloudinaryService _cloudinaryService;

        public EditProfileHandler(
            ICurrentUserService currentUser,
            IRepository<User> userRepo,
            ICloudinaryService cloudinaryService)
        {
            _currentUser = currentUser;
            _userRepo = userRepo;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<RequestResult<bool>> Handle(EditProfileCommand request, CancellationToken cancellationToken)
        {
            if (!_currentUser.IsAuthenticated)
                return RequestResult<bool>.Failure(ErrorCode.Unauthorized, "Not authenticated.");

            var user = await _userRepo.GetByIDAsync(_currentUser.Id ?? Guid.Empty);
            if (user == null)
                return RequestResult<bool>.Failure(ErrorCode.NotFound, "User not found.");

            user.FullName = request.FullName;
            user.PhoneNumber = request.PhoneNumber ?? user.PhoneNumber;
            user.Universtiry = request.University;
            user.FacultyOrDepartment = request.Faculty;

            if (request.DeleteImage && !string.IsNullOrEmpty(user.ImageUrl))
            {
                await _cloudinaryService.DeleteImageAsync(user.ImageUrl);
                user.ImageUrl = string.Empty;
            }
            else if (request.Image != null)
            {
                if (!string.IsNullOrEmpty(user.ImageUrl))
                    await _cloudinaryService.DeleteImageAsync(user.ImageUrl);

                user.ImageUrl = await _cloudinaryService.UploadImageAsync(request.Image);
            }

            if (request.DeleteCV && !string.IsNullOrEmpty(user.CV))
            {
                await _cloudinaryService.DeleteCvAsync(user.CV);
                user.CV = string.Empty;
            }
            else if (request.CV != null)
            {
                if (!string.IsNullOrEmpty(user.CV))
                    await _cloudinaryService.DeleteCvAsync(user.CV);

                user.CV = await _cloudinaryService.UploadCvAsync(request.CV);
            }

            await _userRepo.SaveChangesAsync();

            return RequestResult<bool>.Success(true, "Profile updated successfully");
        }
    }
    }
