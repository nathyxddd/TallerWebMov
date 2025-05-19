using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerWebMov.src.DTOs
{
    public class ProductDTOResponse
    {
        /// <summary>
        /// Título con el nombre del producto (obligatorio).
        /// </summary>
        public required string Title { get; set; } = string.Empty;

        /// <summary>
        /// Precio del producto (por defecto 0).
        /// </summary>
        public required decimal Price { get; set; } = 0;

        /// <summary>
        /// Cantidad de stock disponible (por defecto 0).
        /// </summary>
        public required int Stock { get; set; } = 0;

        /// <summary>
        /// Categoría del producto (obligatoria).
        /// </summary>
        public required string Category { get; set; } = string.Empty;

        /// <summary>
        /// Marca del producto (obligatoria).
        /// </summary>
        public required string Brand { get; set; } = string.Empty;

        /// <summary>
        /// Descripción del producto (obligatoria).
        /// </summary>
        public required string Description { get; set; } = string.Empty;

        /// <summary>
        /// Estado del producto, puede ser "nuevo" o "usado" (obligatorio).
        /// </summary>
        public required string State { get; set; } = string.Empty;

        public required List<string> Galery { get; set; } = []; 
    }
}