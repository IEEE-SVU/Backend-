using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.UserInfo
{
    public interface ICurrentUserService
    {
        bool IsAuthenticated { get; }
        Guid? Id { get; }
        string Name { get; }
        string Role { get; }
    }
}
