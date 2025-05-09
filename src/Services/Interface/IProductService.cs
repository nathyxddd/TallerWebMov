using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerWebM.src.DTOs;
using TallerWebM.src.Models;

namespace TallerWebM.src.Services.Interface
{
    // <summary>
    // Se crea la interfaz para el servicio de productos.
    // </summary>
    public interface IProductService
    {
        // <summary>
        // Se agrega un nuevo producto a la tabla de productos y se guardan los cambios.
        // </summary>
        // <param name="product"> El producto que se desea agregar. </param>
        ProductDto AddProduct(ProductDto productDto);

        // <summary>
        // Se elimina un producto por su ID.
        // </summary>
        // <param name = "productId"> El ID del producto que se desea eliminar. </param>
        // <returns>  El producto eliminado. </returns>
        Product RemoveProduct(int productId);

        // <summary>
        // Se elimina un producto por su nombre.
        // </summary>
        // <param name = "name"> El nombre del producto a eliminar. </param>
        void RemoveProductName(string name);

        // <summary>
        // Se busca un producto por su ID.
        // </summary>
        // <param name = "id"> El ID del producto. </param>
        // <returns>  El producto si se encuentra; de lo contrario, null. </returns>
        ProductDto? GetProductId(int id);

        // <summary>
        // Se obtiene un producto por su nombre.
        // </summary>
        // <param name = "product"> El nombre del producto. </param>
        // <returns>  El producto si se encuentra; de lo contrario, null. </returns>
        ProductDto? GetProductName(string name);

    }
}