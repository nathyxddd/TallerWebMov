using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using TallerWebM.src.Services.Interface;

namespace TallerWebM.src.Services.Implements
{
    public class PhotoService : IPhotoService
    {

        private readonly IConfiguration configuration;
        private readonly Cloudinary _cloudinary;

        public PhotoService(IConfiguration configuration) {
            this.configuration = configuration;
            _cloudinary = new Cloudinary(configuration["Cloudinary:Url"]);
        }

        public async Task<ImageUploadResult> AddPhoto(IFormFile formFile)
        {
            var result = new ImageUploadResult();
            var length = formFile.Length;

            var extension = Path.GetExtension(formFile.FileName);

            if(length > 10485760 || length < 0) {
                throw new Exception("size");
            }

            await using var stream = formFile.OpenReadStream();
            var parameters = new ImageUploadParams {
                File = new FileDescription(formFile.FileName, stream),
                Transformation = new Transformation()
                .Width(700)
                .Height(700)
                .Crop("fill")
                .Gravity("face"),
                Folder = "blackcat"
            };

            return await _cloudinary.UploadAsync(parameters);
        }

        public async Task<DeletionResult> Delete(string id)
        {
            var parameters = new DeletionParams(id);
            return await _cloudinary.DestroyAsync(parameters);
        }

    }

}