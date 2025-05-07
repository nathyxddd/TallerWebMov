using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerWebM.src.Models;

namespace TallerWebM.src.Services.Interface
{
    public interface IProductService
    {
        
        void AddProduct(Product product);

        Product RemoveProduct(int productId);

        void RemoveProductName(string name);

        Product? GetProductId(int id);

        Product? GetProductName(string name);

    }
}