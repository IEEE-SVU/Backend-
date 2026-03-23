using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.CloudinaryManagment;
using Application.Common.Helper;
using Application.Common.UserInfo;
using Application.Enums;
using Application.Services.UserServices.Certificates.DTOs;
using Domain.IRepositories;
using Domain.Models;
using MediatR;

namespace Application.Services.UserServices.GetCertificates.Queries;
public record GetUserCertificatesQuery()
    :IRequest<RequestResult<IEnumerable<CertificateDto>>>;

public class GetUserCertificatesHandler (IRepository<User> repository, ICloudinaryService cloudinaryService, ICurrentUserService currentUser)
    : IRequestHandler<GetUserCertificatesQuery, RequestResult<IEnumerable<CertificateDto>>>
{
    private readonly IRepository<User> _repository = repository;

    private readonly ICloudinaryService _cloudinaryService = cloudinaryService;

    private readonly ICurrentUserService _currentUser = currentUser;

    public async Task<RequestResult<IEnumerable<CertificateDto>>> Handle(GetUserCertificatesQuery request, CancellationToken cancellationToken)
    {
        if(!_currentUser.IsAuthenticated)
            return RequestResult<IEnumerable<CertificateDto>>.Failure(Enums.ErrorCode.Unauthorized, "Not authenticated.");
       

        var user = await _repository.GetByIDAsync(_currentUser.Id ?? Guid.Empty);
        if (user is null)
            return RequestResult<IEnumerable<CertificateDto>>.Failure(ErrorCode.NotFound, "User not found.");

        var certificates = user.Certificates.Select(x => new CertificateDto
        {
            Id = x.Id,
            Title = x.Title,
            IssueDate = x.IssueDate,
            PdfUrl = x.PdfUrl
        });

        return RequestResult<IEnumerable<CertificateDto>>.Success(certificates);
    }
}

