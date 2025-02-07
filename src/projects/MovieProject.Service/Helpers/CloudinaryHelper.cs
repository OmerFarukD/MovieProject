using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace MovieProject.Service.Helpers;

public sealed class CloudinaryHelper : ICloudinaryHelper
{
    private readonly Cloudinary _cloudinary;
    private readonly Account _account;
    private readonly CloudinarySettings _cloudinarySettings;

    public CloudinaryHelper(IOptions<CloudinarySettings> cloudOptions)
    {
        _cloudinarySettings = cloudOptions.Value;

        _account = new Account(_cloudinarySettings.CloudName,
            _cloudinarySettings.ApiKey,
            _cloudinarySettings.ApiSecret
            );


        _cloudinary = new Cloudinary(_account);
    }

    public string UploadImage(IFormFile formFile, string imageDirectory)
    {
        var imageUploadResult = new ImageUploadResult();

        if (formFile.Length > 0)
        {
            using var stream = formFile.OpenReadStream();

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(formFile.Name,stream),
                Folder = imageDirectory
            };

            imageUploadResult =
                _cloudinary.Upload(uploadParams);

            string url = _cloudinary.Api.UrlImgUp.BuildUrl(imageUploadResult.PublicId);
            return url;

        }
        return string.Empty;

    }
}
