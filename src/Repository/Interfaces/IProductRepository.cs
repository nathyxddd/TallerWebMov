using TallerWebM.src.Models;

namespace TallerWebM.src.Repository {

    public interface IProductRepository {

        void AddProduct(Product product);

        void DeleteProduct(Product product);

        Product? GetProduct(int id);

    }

}