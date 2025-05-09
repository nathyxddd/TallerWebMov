using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Microsoft.EntityFrameworkCore;
using TallerWebM.src.Models;

namespace TallerWebM.src.Data.Seeder
{
    /// <summary>
    /// Clase que implementa la inserción de productos falsos en la base de datos, con inyecciones de dependencias para recibir el contexto de la base de datos.
    /// </summary>
    /// <param name="storeContext"> El contexto de la base de datos mediante inserción de dependencias. </param>
    public class ProductSeeder(StoreContext storeContext) : IProductSeeder
    {
        // Generador de números aleatorios.
        private readonly Random _random = new Random();

        // Referencia a la tabla "Products".
        private readonly DbSet<Product> products = storeContext.Products;

        /// <summary>
        /// Método que realiza la inserción de datos falsos.
        /// </summary>
        public void Seed() {

            // Si ya existen productos en la base de datos, no se hace nada.
            if(products.Any()) {
                return;
            }

            // Se crea un generador de productos falsos usando Bogus.
            var faker = new Faker<Product>()
            // Título con nombre de producto.
            .RuleFor(u => u.Title, f => f.Commerce.ProductName())
            // Precio aleatorio entre 250 y 700.
            .RuleFor(u => u.Price, _random.Next(250,700))
            // Stock aleatorio entre 100 y 200.
            .RuleFor(u => u.Stock, _random.Next(100,200))
            // Categoría vacía (por ahora).
            .RuleFor(U => U.Category, "")
            // Marca generada por nombre de empresa.
            .RuleFor(u => u.Brand, f => f.Company.CompanyName())
            // Descripción del producto.
            .RuleFor(u => u.Description, f => f.Commerce.ProductDescription())
            // Estado, puede ser nuevo o usado.
            .RuleFor(u => u.State, f => GenerateState())
            // Fecha reciente de última actualización.
            .RuleFor(u => u.LastUpdated, f => f.Date.Recent());

            // Se generan 100 productos y se agregan a la base de datos.
            faker.Generate(100).ForEach(u => {
                products.Add(u);

            });

            // Se guardan los cambios en la base de datos.
            storeContext.SaveChanges();
    }


    /// <summary>
    /// Método que genera aleatoriamente un estado de producto: "nuevo" o "usado" aleatoriamente.
    /// </summary>
    /// <returns> Cadena con el estado del producto. </returns>
    public string GenerateState(){
        // Genera 0 o 1.
        long aleatorio = _random.NextInt64(0,1);
        return aleatorio == 0? "nuevo": "usado";
        }
    }
}