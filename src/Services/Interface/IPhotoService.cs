using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;

namespace TallerWebM.src.Services.Interface
{
    // <summary>
    // Se crea la interfaz para el servicio de gestión de fotos.
    // </summary>
    public interface IPhotoService
    {

        // <summary>
        // Se sube una imagen al servicio de almacenamiento.
        // </summary>
        // <param name = "formFile"> El archivo de imagen a subir. </param>
        // <returns>  Resultado de la subida, incluyendo URL y estado. </returns>
        public Task<ImageUploadResult> AddPhoto(IFormFile formFile);

        // <summary>
        // Se elimina una imagen del servicio de almacenamiento mediante su ID.
        // </summary>
        // <param name = "id"> El ID de la imagen a eliminar. </param>
        // <returns>  Resultado de la eliminación. </returns>
        public Task<DeletionResult> Delete(string id);

    }

}