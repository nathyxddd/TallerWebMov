using Microsoft.EntityFrameworkCore;
using TallerWebM.src.Data;
using TallerWebM.src.Models;
using TallerWebM.src.Repository;

public class ProductRepository : IProductRepository
{
    /// <summary>
    /// Tabla de productos.
    /// </summary>
    private readonly DbSet<Product> products;

    /// <summary>
    /// Contexto de la base de datos.
    /// </summary>
    private readonly StoreContext storeContext;

    /// <summary>
    /// Constructor del repositorio.
    /// </summary>
    /// <param name="storeContext"></param>
    public ProductRepository(StoreContext storeContext)
    {
        this.storeContext = storeContext;
        products = storeContext.Products;
    }

    /// <summary>
    /// Se agrega un nuevo producto al repositorio.
    /// </summary>
    /// <param name="product"> E l producto que se desea agregar. </param>
    public void AddProduct(Product product)
    {
        products.Add(product);
        
    }

    /// <summary>
    /// Se elimina un producto del repositorio.
    /// </summary>
    /// <param name="product"> El producto que se desea eliminar. </param>
    public void DeleteProduct(Product product)
    {
        products.Remove(product);
        
    }

    /// <summary>
    /// Se obtiene un producto por su ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns> El producto correspondiente si se encuentra, o null si no existe. </returns>
    public Product? GetProduct(int id)
    {
        return products.Find(id);
    }

    /// <summary>
    /// Se guardan los cambios.
    /// </summary>
    public void Save()
    {
        storeContext.SaveChanges();
    }
}