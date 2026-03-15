using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Interview : BaseModel
    {
        public Guid ApplicationId { get; set; }
        public DateTime Date { get; set; }
        public string MeetingLink { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public bool IsOnline { get; set; } = true;

        #region Navigation Properties
        public virtual MembershipApplication Application { get; set; } = null!;
        #endregion
    }
}
