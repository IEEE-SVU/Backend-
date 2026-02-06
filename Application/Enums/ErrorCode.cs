using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Enums
{
    public enum ErrorCode
    {
        NoError,
        NotFound,
        ValidationError,
        Unauthorized,
        Forbidden,
        InternalServerError,
        Conflict,
        BadRequest
    }
}
