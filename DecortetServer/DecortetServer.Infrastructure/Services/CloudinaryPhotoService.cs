using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DecortetServer.Core.Interfaces.Services;
using DecortetServer.Infrastructure.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecortetServer.Infrastructure.Services
{
    public class CloudinaryPhotoService : IPhotoService
    {
        private readonly ICloudinary _cloudinary;

        public CloudinaryPhotoService(IOptions<CloudinarySettigns> config)
        {
            var acc = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret);
            _cloudinary = new Cloudinary(acc);
        }

        public async Task<string> AddPhotoAsync(IFormFile file)
        {
            var name = Guid.NewGuid().ToString();
            name = string.Concat(name, Path.GetExtension(file.Name));
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(name, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);

            }
            return uploadResult.Uri.ToString();
        }

        public async Task<IEnumerable<string>> AddPhotosAsync(IEnumerable<IFormFile> files)
        {
            var results = new List<string>();

            foreach (var file in files)
            {
                var uploadResult = await AddPhotoAsync(file);
                results.Add(uploadResult);
            }

            return results;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string imageLink)
        {
            var lastSlashIndex = imageLink.LastIndexOf('/');
            var publicId = imageLink.Substring(lastSlashIndex + 1);

            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result;
        }
    }
}
