using TallerWebM.src.DTOs;
using TallerWebM.src.Models;

public interface IProductCreationMapper {

    public Product Mapper(ProductDto productDto);

    public ProductDto Mapper(Product product);

}