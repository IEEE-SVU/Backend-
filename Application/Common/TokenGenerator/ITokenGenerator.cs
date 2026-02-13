using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.TokenGenerator
{
    public interface ITokenGenerator
    {
        public  Task<string> GenerateToken(Guid Id);
    }
}
