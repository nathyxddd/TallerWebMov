using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Microsoft.EntityFrameworkCore;
using TallerWebM.src.Models;

namespace TallerWebM.src.Data.Seeder
{
    public class ProductSeeder(StoreContext storeContext) : IProductSeeder
    {
        private readonly Random _random = new Random();
        private readonly DbSet<Product> products = storeContext.Products;

        public void Seed() {
            if(products.Any()) {
                return;
            }

            var faker = new Faker<Product>()
            .RuleFor(u => u.Title, f => f.Commerce.ProductName())
            .RuleFor(u => u.Price, _random.Next(250,700))
            .RuleFor(u => u.Stock, _random.Next(100,200))
            .RuleFor(U => U.Category, "")
            .RuleFor(u => u.Brand, f => f.Company.CompanyName())
            .RuleFor(u => u.Description, f => f.Commerce.ProductDescription())
            .RuleFor(u => u.State, f => GenerateState())
            .RuleFor(u => u.LastUpdated, f => f.Date.Recent());

            faker.Generate(100).ForEach(u => {
                products.Add(u);
                
            });

            
            storeContext.SaveChanges();
    }

    public string GenerateState(){
        long aleatorio = _random.NextInt64(0,1);
        return aleatorio == 0? "nuevo": "usado";
        }
    }
}