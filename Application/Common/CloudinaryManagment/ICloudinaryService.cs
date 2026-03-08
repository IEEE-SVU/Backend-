using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.CloudinaryManagment
{
    public interface ICloudinaryService
    {
        Task<string> UploadImageAsync(IFormFile file);
        Task<string> UploadCvAsync(IFormFile file);
        Task<bool> DeleteImageAsync(string imageUrl);
        Task<bool> DeleteCvAsync(string cvUrl);
    }
}
