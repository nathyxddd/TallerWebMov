using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TallerWebM.src.DTOs;
using TallerWebM.src.Services.Interface;
using TallerWebMov.src.DTOs;


namespace TallerWebM.src.Controllers
{
    // Se define la ruta basa del controlador.
    [Route("api/[controller]")]
    [ApiController]

    /// <summary>
    /// Controlador encargado de manejar las operaciones relacionadas con los productos.
    /// </summary>
    public class ProductController : ControllerBase
    {
        // Se declara una variable para acceder a los servicios de producto.
        private readonly IProductService productService;

        /// <summary>
        /// Constructor que inicializa el controlador con el servicio de productos mediante inyección de dependencias.
        /// </summary>
        /// <param name="productService"> Servicio de productos inyectado. </param>
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }


        /// <summary>
        /// Método HTTP POST para agregar un nuevo producto al sistema.
        /// </summary>
        /// <param name="productDto"> Objeto que contiene los datos del producto a agregar. </param>
        /// <returns> El producto agregado. </returns>
        [HttpPost]
        [Route("/product/add")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductDTOResponse>> Add([FromForm] ProductDto product,
        [FromForm] List<IFormFile> images)
        {
            try
            {
                return Ok(await productService.AddProduct(product, images));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Método HTTP DELETE para eliminar un producto por su ID.
        /// </summary>
        /// <param name="id">ID del producto que se desea eliminar. </param>
        /// <returns> El producto eliminado si existe, o un mensaje de error si no se encuentra. </returns>
        [HttpDelete]
        [Route("/product/remove/{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<ProductDTOResponse> Remove(int id)
        {
            try
            {
                // Se llama al servicio para eliminar el producto.
                return Ok(productService.RemoveProduct(id));
            }
            catch (Exception e)
            {
                // Si ocurre un error y no se encuentra el producto, se devuelve un código 404: Recurso no encontrado.
                return NotFound("Producto no encontrado: " + e.Message);
            }
        }


        /// <summary>
        /// Método HTTP GET para obtener un producto por su ID.
        /// </summary>
        /// <param name="id"> ID del producto que se desea obtener. </param>
        /// <returns> El producto encontrado o un mensaje de error si no existe. </returns>
        [HttpGet]
        [Route("/product/get/{id}")]
        public ActionResult<ProductDTOResponse> Get(int id)
        {
            try
            {
                // Se llama al servicio para obtener un producto por ID.
                return Ok(productService.GetProductId(id));
            }
            catch (Exception)
            {
                // Si ocurre un error y no se encuentra el producto, se devuelve un código 404: Recurso no encontrado.
                return NotFound("Producto no encontrado");
            }
        }
        
        [HttpGet]
        [Route("/product/search")]
        public ActionResult<List<ProductDTOResponse>> Search(int page, int? elements, string? category, int? minRange, int? maxRange, string? state, string? brand, bool? isOrderedAscending, bool? isOrderedDescending)
        {
            return Ok(productService.Search(page, elements, category, minRange, maxRange, state, brand, isOrderedAscending, isOrderedDescending));
        }

    }

}