using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TallerWebM.src.Data;
using TallerWebM.src.DTOs;
using TallerWebM.src.Models;
using TallerWebM.src.Services.Interface;

namespace TallerWebM.src.Services.Implements
{
    // <summary>
    // Se implementa la interfaz para el servicio de productos.
    // </summary>
    public class ProductService : IProductService
    {

        // Contexto de la base de datos que permite el acceso a todas las tablas.
        private readonly StoreContext storeContext;

        // Tabla de productos, se accede como DbSet<Product>.
        private readonly DbSet<Product> products;

        private readonly IProductCreationMapper productCreationMapper;

        // <summary>
        // Controlador que inicializa el servicio con el StoreContext y la tabla de productos.
        // </summary>
        // <param name="storeContext"> El contexto de la base de datos. </param>
        // <param name="products"> La tabla de productos. </param>
        public ProductService(StoreContext storeContext,
        IProductCreationMapper productCreationMapper) {
            this.storeContext = storeContext;
            this.products = storeContext.Products;
            this.productCreationMapper = productCreationMapper;
        }

        // <summary>
        // Se agrega un nuevo producto a la tabla de productos y se guardan los cambios.
        // </summary>
        // <param name="product"> El producto que se desea agregar. </param>
        public ProductDto AddProduct(Product product)
        {
            // Se agrega el product al DbSet.
            products.Add(product);

            var dto = productCreationMapper.Mapper(product);

            // Se guardan los cambios en la base de datos.
            storeContext.SaveChanges();
            return dto;
        }

        // <summary>
        // Se busca un producto por su ID.
        // </summary>
        // <param name="id"> El ID del producto. </param>
        // <returns> El producto si se encuentra; de lo contrario, null. </returns>
        public ProductDto? GetProductId(int id)
        {
            // Usa EF Core para buscar.
            var productFind = products.Find(id);
            if(productFind == null) {
                return null;
            }
           return productCreationMapper.Mapper(productFind);
        }

        // <summary>
        // Se obtiene un producto por su nombre.
        // </summary>
        // <param name = "product"> El nombre del producto. </param>
        // <returns>  El producto si se encuentra; de lo contrario, null. </returns>
        public ProductDto? GetProductName(string name)
        {
            // Filtra productos por nombre y retorna el primero que coincide.
            var productFind = products.Where(p => p.Title == name)
            .First();

            if(productFind == null) {
                return null;
            }

            return productCreationMapper.Mapper(productFind);
        }

        // <summary>
        // Se elimina un producto por su ID.
        // </summary>
        // <param name = "productId"> El ID del producto que se desea eliminar. </param>
        // <returns>  El producto eliminado. </returns>
        public Product RemoveProduct(int productId)
        {
            // Busca el producto por ID.
            var productSearched = products.Find(productId);

            // Si no existe, lanza una excepción.
            if(productSearched == null) {
                throw new Exception("not_exists");
            }

            // Se elimina del DbSet.
            products.Remove(productSearched);

            // Retorna el producto eliminado.
            return productSearched;
        }

        // <summary>
        // Se elimina un producto por su nombre.
        // </summary>
        // <param name = "name"> El nombre del producto a eliminar. </param>
        public void RemoveProductName(string name)
        {
            // Buscar por nombre.
            var productSearched = products.Where(p => p.Title == name)
            .First();

            // Si no se encuentra, lanza un error.
            if(productSearched == null) {
                throw new Exception("not_exists");
            }

            // Elimina el producto.
            products.Remove(productSearched);
        }

    }

}