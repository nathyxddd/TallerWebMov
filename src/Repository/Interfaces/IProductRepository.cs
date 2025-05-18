using TallerWebM.src.Models;

namespace TallerWebM.src.Repository {

    public interface IProductRepository
    {
        /// <summary>
        /// Se agrega un nuevo producto al repositorio.
        /// </summary>
        /// <param name="product"> E l producto que se desea agregar. </param>
        void AddProduct(Product product);

        /// <summary>
        /// Se elimina un producto del repositorio.
        /// </summary>
        /// <param name="product"> El producto que se desea eliminar. </param>
        void DeleteProduct(Product product);

        /// <summary>
        /// Se obtiene un producto por su ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> El producto correspondiente si se encuentra, o null si no existe. </returns>
        Product? GetProduct(int id);

        /// <summary>
        /// Se guardan los cambios.
        /// </summary>
        void Save();

    }

}