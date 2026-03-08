using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.CloudinaryManagment
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IOptions<CloudinarySettings> config)
        {
            var account = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );
            _cloudinary = new Cloudinary(account);
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0) return string.Empty;

            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = "IEEE_Images", 
                Transformation = new Transformation().Height(500).Width(500).Crop("fill") 
            };

            var result = await _cloudinary.UploadAsync(uploadParams);
            return result.SecureUrl.ToString();
        }

        public async Task<string> UploadCvAsync(IFormFile file)
        {
            if (file == null || file.Length == 0) return string.Empty;

            using var stream = file.OpenReadStream();
            var uploadParams = new RawUploadParams 
            {
                File = new FileDescription(file.FileName, stream),
                Folder = "IEEE_CVs"
            };

            var result = await _cloudinary.UploadAsync(uploadParams);
            return result.SecureUrl.ToString();
        }

        public async Task<bool> DeleteImageAsync(string imageUrl)
        {
            return await DeleteFileAsync(imageUrl, ResourceType.Image);
        }

        public async Task<bool> DeleteCvAsync(string cvUrl)
        {
            return await DeleteFileAsync(cvUrl, ResourceType.Raw);
        }

        private async Task<bool> DeleteFileAsync(string url, ResourceType type)
        {
            if (string.IsNullOrEmpty(url)) return false;

            var publicId = GetPublicIdFromUrl(url);
            if (string.IsNullOrEmpty(publicId)) return false;

            var deleteParams = new DeletionParams(publicId) { ResourceType = type };
            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result.Result == "ok";
        }

        private string GetPublicIdFromUrl(string url)
        {
            try
            {
                var uri = new Uri(url);
                var segments = uri.Segments;
                var pathWithoutVersion = string.Join("", segments.Skip(5));
                return Path.ChangeExtension(pathWithoutVersion, null);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}

