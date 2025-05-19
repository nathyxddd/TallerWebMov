using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TallerWebM.src.Data;
using TallerWebM.src.DTOs;
using TallerWebM.src.Models;
using TallerWebM.src.Repository;
using TallerWebM.src.Services.Interface;
using TallerWebMov.src.DTOs;

namespace TallerWebM.src.Services.Implements
{
    // <summary>
    // Se implementa la interfaz para el servicio de productos.
    // </summary>
    public class ProductService : IProductService
    {

        // Tabla de productos, se accede como DbSet<Product>.
        private readonly DbSet<Product> products;

        //
        private readonly IProductCreationMapper productCreationMapper;

        //
        private readonly IPhotoService photoService;

        //
        private readonly IProductRepository productRepository;

        /// <summary>
        /// Controlador que inicializa el servicio con el StoreContext y la tabla de productos.
        /// </summary>
        /// <param name="storeContext"> El contexto de la base de datos. </param>
        /// <param name="products"> La tabla de productos. </param>
        public ProductService(StoreContext storeContext,
        IProductCreationMapper productCreationMapper,
        IPhotoService photoService,
        IProductRepository productRepository) {
            this.products = storeContext.Products;
            this.productCreationMapper = productCreationMapper;
            this.photoService = photoService;
            this.productRepository = productRepository;
        }

        /// <summary>
        /// Se agrega un nuevo producto a la tabla de productos y se guardan los cambios.
        /// </summary>
        /// <param name="product"> El producto que se desea agregar. </param>
        /// <param name="formFiles"> Lista de archivos (imágenes) asociados al producto. </param>
        /// <returns> El producto agregado como DTO. </returns>
        public async Task<ProductDto> AddProduct(ProductDto product, List<IFormFile> images)
        {

            var urls = "";

            foreach(var image in images) {
               var result = await photoService.AddPhoto(image);
               urls += result.Url.AbsoluteUri + " ";
            }

            var productCreated = productCreationMapper.Mapper(product, urls);

            // Se guardan los cambios en la base de datos.
            productRepository.AddProduct(productCreated);
            productRepository.Save();
            return product;
        }

        /// <summary>
        /// Se busca un producto por su ID.
        /// </summary>
        /// <param name = "id"> El ID del producto. </param>
        /// <returns>  El producto encontrado como DTO; de lo contrario, null. </returns>
        public ProductDTOResponse? GetProductId(int id)
        {
            // Usa EF Core para buscar.
            var productFind = products.Find(id);
            if(productFind == null) {
                return null;
            }
           return productCreationMapper.Mapper(productFind);
        }

        /// <summary>
        /// Se obtiene un producto por su nombre.
        /// </summary>
        /// <param name = "product"> El nombre del producto. </param>
        /// <returns>  El producto encontrado como DTO; de lo contrario, null. </returns>
        public ProductDTOResponse? GetProductName(string name)
        {
            // Filtra productos por nombre y retorna el primero que coincide.
            var productFind = products.Where(p => p.Title == name)
            .First();

            if(productFind == null) {
                return null;
            }

            return productCreationMapper.Mapper(productFind);
        }

        /// <summary>
        /// Se elimina un producto por su ID.
        /// </summary>
        /// <param name = "productId"> El ID del producto que se desea eliminar. </param>
        /// <returns>  El producto eliminado. </returns>
        public ProductDTOResponse? RemoveProduct(int productId)
        {
            // Busca el producto por ID.
            var productSearched = products.Find(productId);

            // Si no existe, lanza una excepción.
            if (productSearched == null) {
                throw new Exception("not_exists");
            }

            if (productSearched.Galery != "")
            {
                var imagesUrl = productSearched.Galery.Trim().Split(" ");
                foreach (var url in imagesUrl)
                {
                    var id = url.Split("blackcat/")[1];
                    id = id.Replace(".jpg", "");
                    id = "blackcat/" + id;
                    photoService.Delete(id);
                }
            }
            

            // Se elimina del DbSet.
            productRepository.DeleteProduct(productSearched);
            productRepository.Save();

            // Retorna el producto eliminado.
            return productCreationMapper.Mapper(productSearched);
        }

        /// <summary>
        /// Elimina un producto según su nombre.
        /// </summary>
        /// <param name="name">El nombre del producto que se desea eliminar.</param>
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
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="elements"></param>
        /// <param name="category"></param>
        /// <param name="minRange"></param>
        /// <param name="maxRange"></param>
        /// <param name="state"></param>
        /// <param name="brand"></param>
        /// <param name="isOrderedAscending"></param>
        /// <param name="isOrderedDescending"></param>
        /// <returns> Una lista de productos que cumplen con los criterios especificados. </returns>
        public List<ProductDTOResponse> Search(int page, int? elements, string? category, int? minRange, int? maxRange, string? state, string? brand, bool? isOrderedAscending, bool? isOrderedDescending)
        {

            IEnumerable<Product> search = products;

            if (category != null)
            {
                search = products.Where(p => p.Category == category);
            }

            if (minRange != null && maxRange != null)
            {
                search = products.Where(p => p.Price > minRange && p.Price <= maxRange);
            }

            if (state != null)
            {
                search = products.Where(p => p.State != state);
            }

            if (brand != null)
            {
                search = products.Where(p => p.Brand == brand);
            }

            if (isOrderedAscending != null)
            {
                search = products.OrderBy(p => p.Price);
            }

            if (isOrderedDescending != null)
            {
                search = products.OrderByDescending(p => p.Price);
            }

            int total = 10;
            if (elements != null)
            {
                total = elements.Value;
            }

            search = products.Skip((page - 1) * 10).Take(10);

            var dtos = new List<ProductDTOResponse>();

            var list = search.ToList();
            foreach (var e in list)
            {
                var productDto = productCreationMapper.Mapper(e);
                dtos.Add(productDto);
            }
            return dtos;
        }

    }

}