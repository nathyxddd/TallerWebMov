using System.Composition;
using TallerWebM.src.DTOs;
using TallerWebM.src.Models;

public class ProductCreationMapper : IProductCreationMapper
{

    public Product Mapper(ProductDto productDto)
    {
        return new Product {
            Title = productDto.Title,
            State = productDto.State,
            Price = productDto.Price,
            Category = productDto.Category,
            Brand = productDto.Brand,
            Description = productDto.Description,
            Stock = productDto.Stock
        };
    }

    public ProductDto Mapper(Product product)
    {
        return new ProductDto {
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