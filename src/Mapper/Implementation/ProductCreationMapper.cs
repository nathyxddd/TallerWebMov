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

}