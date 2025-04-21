using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerWebM.src.Models
{
    public class Product
    {
        public int Id { get; set; } = 0;
        public required string Title { get; set; } = string.Empty;

        public decimal Price { get; set; } = 0;

        public int Stock { get; set; } = 0;

        public required string Category { get; set; } = string.Empty;

        public required string Brand { get; set; } = string.Empty;

        public required string Description { get; set; } = string.Empty;

        public required string State { get; set; } = string.Empty;

        public string []? Galery { get; set; } = [];

        public required DateTime LastUpdated { get; set; } = DateTime.Now;

    }
}