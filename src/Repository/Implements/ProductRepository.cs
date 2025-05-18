using Microsoft.EntityFrameworkCore;
using TallerWebM.src.Data;
using TallerWebM.src.Models;
using TallerWebM.src.Repository;

public class ProductRepository : IProductRepository
{

    private readonly DbSet<Product> products;
    private readonly StoreContext storeContext;

    public ProductRepository(StoreContext storeContext) {
        this.storeContext = storeContext;
        products = storeContext.Products;
    }

    public void AddProduct(Product product)
    {
       products.Add(product);
       storeContext.SaveChanges();
    }

    public void DeleteProduct(Product product)
    {
        products.Remove(product);
        storeContext.SaveChanges();
    }

    public Product? GetProduct(int id)
    {
        return products.Find(id);
    }

}