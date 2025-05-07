using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TallerWebM.src.Models;
using TallerWebM.src.Services.Interface;

namespace TallerWebM.src.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController
    {
        
        private readonly IProductService productService;

        public ProductController(IProductService productService) {
            this.productService = productService;
        }

    }

}