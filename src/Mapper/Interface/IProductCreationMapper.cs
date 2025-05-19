using TallerWebM.src.DTOs;
using TallerWebM.src.Models;
using TallerWebMov.src.DTOs;

public interface IProductCreationMapper {

    /// <summary>
    /// 
    /// </summary>
    /// <param name="productDto"></param>
    /// <param name="images"></param>
    /// <returns></returns>
    public Product Mapper(ProductDto productDto, string images);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    public ProductDTOResponse Mapper(Product product);

}