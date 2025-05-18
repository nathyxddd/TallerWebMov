using TallerWebM.src.DTOs;
using TallerWebM.src.Models;

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
    public ProductDto Mapper(Product product);

}