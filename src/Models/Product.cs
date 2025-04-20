using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerWebM.src.Models
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public required string Description { get; set; }

        public decimal Price { get; set; }

        public required string Category { get; set; }

        public string []? Urls { get; set; }

        public int Stock { get; set; }

        public required string Brand { get; set; }
    }
}