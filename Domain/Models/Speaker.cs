using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Speaker : BaseModel
    {
        public string FullName { get; set; }
        public string? Bio { get; set; }
        public string? PhotoUrl { get; set; }
        public Guid WorkshopId { get; set; }
        #region Navigation Properties
        public virtual Workshop Workshop { get; set; }
        #endregion 
    }
}
