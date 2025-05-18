using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerWebM.src.DTOs;
using TallerWebM.src.Models;

namespace TallerWebM.src.Services.Interface
{
    /// <summary>
    /// Se crea la interfaz para el servicio de productos.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Se agrega un nuevo producto a la tabla de productos y se guardan los cambios.
        /// </summary>
        /// <param name="product"> El producto que se desea agregar. </param>
        /// <param name="formFiles"> Lista de archivos (imágenes) asociados al producto. </param>
        /// <returns> El producto agregado como DTO. </returns>
        Task<ProductDto> AddProduct(ProductDto product, List<IFormFile> formFiles);

        /// <summary>
        /// Se elimina un producto por su ID.
        /// </summary>
        /// <param name = "productId"> El ID del producto que se desea eliminar. </param>
        /// <returns>  El producto eliminado. </returns>
        Product RemoveProduct(int productId);

        /// <summary>
        /// Elimina un producto según su nombre.
        /// </summary>
        /// <param name="name">El nombre del producto que se desea eliminar.</param>
        void RemoveProductName(string name);

        /// <summary>
        /// Se busca un producto por su ID.
        /// </summary>
        /// <param name = "id"> El ID del producto. </param>
        /// <returns>  El producto encontrado como DTO; de lo contrario, null. </returns>
        ProductDto? GetProductId(int id);

        /// <summary>
        /// Se obtiene un producto por su nombre.
        /// </summary>
        /// <param name = "product"> El nombre del producto. </param>
        /// <returns>  El producto encontrado como DTO; de lo contrario, null. </returns>
        ProductDto? GetProductName(string name);

        /// <summary>
        /// Se realiza una búsqueda avanzada de productos según los siguientes parametros:
        /// </summary>
        /// <param name="page">Número de página de resultados. </param>
        /// <param name="elements"> Cantidad de elementos por página. </param>
        /// <param name="category"> Categoría del producto. </param>
        /// <param name="minRange"> Rango mínimo de precio. </param>
        /// <param name="maxRange"> Rango máximo de precio. </param>
        /// <param name="state"> Estado del producto (nuevo o usado). </param>
        /// <param name="brand"> Marca del producto. </param>
        /// <param name="isOrderedAscending"> Indica si los resultados deben estar ordenados ascendentemente. </param>
        /// <param name="isOrderedDescending"> Indica si los resultados deben estar ordenados descendentemente. </param>
        /// <returns> Una lista de productos que cumplen con los criterios especificados. </returns>
        List<ProductDto> Search(int page, int? elements, string? category,int? minRange, int? maxRange, string? state, string? brand, bool? isOrderedAscending, bool? isOrderedDescending);

    }
}