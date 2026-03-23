using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserServices.Certificates.DTOs;
public class CertificateDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime IssueDate { get; set; }
    public string PdfUrl { get; set; } = string.Empty;
}
