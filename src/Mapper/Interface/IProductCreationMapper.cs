using TallerWebM.src.DTOs;
using TallerWebM.src.Models;

public interface IProductCreationMapper {

    public Product Mapper(ProductDto productDto, string images);

    public ProductDto Mapper(Product product);

}