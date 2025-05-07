using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TallerWebM.src.Data;
using TallerWebM.src.Models;
using TallerWebM.src.Services.Interface;

namespace TallerWebM.src.Services.Implements
{
    public class ProductService : IProductService
    {

        private readonly StoreContext storeContext;
        private readonly DbSet<Product> products;

        public ProductService(StoreContext storeContext, 
        DbSet<Product> products) {
            this.storeContext = storeContext;
            this.products = products;
        }

        public void AddProduct(Product product)
        {
            products.Add(product);
            storeContext.SaveChanges();
        }

        public Product? GetProductId(int id)
        {
           return products.Find(id);
        }

        public Product? GetProductName(string name)
        {
            return products.Where(p => p.Title == name)
            .First();
        }

        public void RemoveProduct(int productId)
        {
            var productSearched = GetProductId(productId);
            if(productSearched == null) {
                throw new Exception("not_exists");
            }
            products.Remove(productSearched);
        }

        public void RemoveProductName(string name)
        {
            
            var productSearched = GetProductName(name);
            if(productSearched == null) {
                throw new Exception("not_exists");
            }
            products.Remove(productSearched);
        }

    }

}