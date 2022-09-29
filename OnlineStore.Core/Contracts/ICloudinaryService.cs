using Microsoft.AspNetCore.Http;

namespace OnlineStore.Core.Contracts
{
    public interface ICloudinaryService
    {
        Task<string> UploadPicture(IFormFile picture);
    }
}