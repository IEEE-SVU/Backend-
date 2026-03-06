using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Certificate : BaseModel
    {
        public string Title { get; set; }     
        public DateTime IssueDate { get; set; } 
        public string PdfUrl { get; set; }     

        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
