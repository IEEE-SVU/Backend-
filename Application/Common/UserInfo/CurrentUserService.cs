using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.UserInfo
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

        public bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;

        public Guid? Id
        {
            get
            {
                var idClaim = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return Guid.TryParse(idClaim, out Guid id) ? id : null;
            }
        }

        public string Name => User?.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;

        public string Role => User?.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;
    }
}
