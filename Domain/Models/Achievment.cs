using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models;
public class Achievment : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public Guid CommunityId { get; set; }
    public virtual Community Community { get; set; } = new();
}
