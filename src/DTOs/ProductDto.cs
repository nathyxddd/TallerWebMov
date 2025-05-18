using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TallerWebM.src.DTOs
{

    /// <summary>
    /// Clase DTO (Data Transfer Object) usada para transferir datos de productos.
    /// </summary>
    public class ProductDto
    {
        /// <summary>
        /// Título con el nombre del producto (obligatorio).
        /// </summary>
        public required string Title { get; set; } = string.Empty;

        /// <summary>
        /// Precio del producto (por defecto 0).
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "El precio debe ser un valor positivo")]
        public decimal Price { get; set; } = 0;

        /// <summary>
        /// Cantidad de stock disponible (por defecto 0).
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "El stock debe ser un valor positivo")]
        public int Stock { get; set; } = 0;

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
        [RegularExpression(@"^nuevo|usado")]
        public required string State { get; set; } = string.Empty;

    }

}