using TallerWebM.src.DTOs;
using TallerWebM.src.Models;

public class ProductCreationMapper : IProductCreationMapper
{
    /// <summary>
    /// Implementación del método Mapper que convierte un ProductDto en un Product.
    /// </summary>
    /// <param name="productDto"> El objeto ProductDto que contiene los datos del producto. </param>
    /// <param name="images"> Una cadena que representa las imágenes del producto. </param>
    /// <returns> Un objeto Product con los datos mapeados desde el DTO y las imágenes. </returns>
    public Product Mapper(ProductDto productDto, string images)
    {
        return new Product
        {
            Title = productDto.Title,
            State = productDto.State,
            Price = productDto.Price,
            Category = productDto.Category,
            Brand = productDto.Brand,
            Description = productDto.Description,
            Stock = productDto.Stock,
            Galery = images
        };
    }
    
    /// <summary>
    /// Metodo que convierte un objeto Product del modelo en un objeto ProductDto, para enviar los datos del producto al cliente.
    /// </summary>
    /// <param name="product"> El objeto Product que se desea convertir. </param>
    /// <returns> Un objeto ProductDto con los datos mapeados desde el modelo. </returns>
    public ProductDto Mapper(Product product)
    {
        return new ProductDto
        {
            Title = product.Title,
            Price = product.Price,
            Stock = product.Stock,
            Category = product.Category,
            Brand = product.Brand,
            Description = product.Description,
            State = product.State
        };
    }

}