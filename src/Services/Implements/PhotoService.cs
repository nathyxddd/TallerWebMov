using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using TallerWebM.src.Services.Interface;

namespace TallerWebM.src.Services.Implements
{
    // <summary>
    // Se implementa la interfaz para el servicio de gestión de fotos.
    // </summary>
    public class PhotoService : IPhotoService
    {


        private readonly IConfiguration configuration;

        // Instancia de Cloudinary para gestionar imágenes.
        private readonly Cloudinary _cloudinary;

        /// <summary>
        /// Constructor del servicio que crea una instancia de Cloudinary.
        /// </summary>
        /// <param name="configuration"> Recibe una configuración. </param>
        public PhotoService(IConfiguration configuration) {
            this.configuration = configuration;
            _cloudinary = new Cloudinary(configuration["Cloudinary:Url"]);
        }

        // <summary>
        // Se sube una imagen al servicio de almacenamiento.
        // </summary>
        // <param name = "formFile"> El archivo de imagen a subir. </param>
        // <returns>  Resultado de la subida, incluyendo URL y estado. </returns>
        public async Task<ImageUploadResult> AddPhoto(IFormFile formFile)
        {
            // Resultado que se devolverá tras subir la imagen.
            var result = new ImageUploadResult();

            // Se obtiene el tamaño del archivo.
            var length = formFile.Length;

            // Se extrae la extensión del archivo (por ejemplo, .jpg, .png).
            var extension = Path.GetExtension(formFile.FileName);

            // Se verficia que el archivo no pese más de 10MB.
            if(length > 10485760 || length < 0) {
                // Excepción si el archivo es demasiado grande.
                throw new Exception("size");
            }

            // Se abre el archivo como un stream (flujo de datos).
            await using var stream = formFile.OpenReadStream();

            // Se definen los parámetros para subir la imagen.
            var parameters = new ImageUploadParams {
                // Se define el archivo.
                File = new FileDescription(formFile.FileName, stream),
                Transformation = new Transformation()
                // Se redimensiona a 700px de ancho.
                .Width(700)
                // Se redimensiona a 700px de alto.
                .Height(700)
                // Se recorta para llenar el cuadro sin distorsionar.
                .Crop("fill")
                // Centra en la cara si hay una.
                .Gravity("face"),
                // Carpeta destino en Cloudinary.
                Folder = "blackcat"
            };

            // Se sube la imagen de forma asíncrona y se retorna el resultado.
            return await _cloudinary.UploadAsync(parameters);
        }

        // <summary>
        // Se elimina una imagen del servicio de almacenamiento mediante su ID.
        // </summary>
        // <param name = "id"> El ID de la imagen a eliminar. </param>
        // <returns>  Resultado de la eliminación. </returns>
        public async Task<DeletionResult> Delete(string id)
        {
            // Se definen los parámetros para eliminar la imagen.
            var parameters = new DeletionParams(id);

            // Se realiza la eliminación de forma asíncrona y se retorna el resultado.
            return await _cloudinary.DestroyAsync(parameters);
        }

    }

}