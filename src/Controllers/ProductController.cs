using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TallerWebM.src.DTOs;
using TallerWebM.src.Models;
using TallerWebM.src.Services.Interface;

namespace TallerWebM.src.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        
        private readonly IProductService productService;

        public ProductController(IProductService productService) {
            this.productService = productService;
        }

        [HttpPost]
        [Route("/product/add")]
        public ActionResult<ProductDto> Add([FromBody] ProductDto productDto) {

        }

        [HttpDelete]
        [Route("/product/remove")]
        public ActionResult<ProductDto> Remove(int id) {
            try {
                return Ok(productService.RemoveProduct(id));
            } catch (Exception) {
                return NotFound("Producto no encontrado");
            }
        }

        [HttpPost]
        public ActionResult<ProductDto> Get(int id) {
            try
            {
                return Ok(productService.GetProductId(id));
            } catch (Exception)
            {
                return NotFound("Producto no encontrado");
            }
        }




    }

}