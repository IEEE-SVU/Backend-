using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class MembershipApplication : BaseModel
    {
        public Guid UserId { get; set; }
        public Guid CommunityId { get; set; }
        public ApplicationStatus Status { get; set; } = ApplicationStatus.Applied;

        #region Navigation Properties
        public virtual User User { get; set; } = null!;
        public virtual Community Community { get; set; } = null!;
        public virtual Interview? Interview { get; set; }
        #endregion
    }
}
