using Microsoft.AspNetCore.Http;

namespace MovieProject.Service.Helpers;

public interface ICloudinaryHelper
{

    string UploadImage(IFormFile formFile, string imageDirectory);
}
