using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using DecortetServer.Core.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;

namespace DecortetServer.Core.Interfaces.Services
{
    public interface IPhotoService
    {
        Task<string> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
        Task<IEnumerable<string>> AddPhotosAsync(IEnumerable<IFormFile> files);
        
    }
}
