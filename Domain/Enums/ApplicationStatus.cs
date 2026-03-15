using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum ApplicationStatus
    {
        Applied = 1,
        PendingInterview = 2,
        Interviewed = 3,
        ActiveMember = 4,
        Rejected = 5
    }
}
