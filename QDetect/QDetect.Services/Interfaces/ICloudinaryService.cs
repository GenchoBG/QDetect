using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace QDetect.Services.Interfaces
{
    public interface ICloudinaryService
    {
        Task<string> UploadPictureAsync(IFormFile pictureFile, string fileName);
    }
}
