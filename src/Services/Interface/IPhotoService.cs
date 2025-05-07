using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;

namespace TallerWebM.src.Services.Interface
{
    public interface IPhotoService
    {

        public Task<ImageUploadResult> AddPhoto(IFormFile formFile);

        public Task<DeletionResult> Delete(string id); 

    }

}