using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerWebM.src.Models
{

    /// <summary>
    /// Clase que representa un producto dentro del sistema.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// ID del producto.
        /// </summary>
        public int Id { get; set; } = 0;

        /// <summary>
        /// Título con nombre del producto (obligatorio).
        /// </summary>
        public required string Title { get; set; } = string.Empty;

        /// <summary>
        /// Precio del producto (por defecto 0).
        /// </summary>
        public decimal Price { get; set; } = 0;

        /// <summary>
        /// Cantidad de stock disponible (por defecto 0).
        /// </summary>
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
        /// Descripción del producto (obligatorio).
        /// </summary>
        public required string Description { get; set; } = string.Empty;

        /// <summary>
        /// Estado del producto, puede ser "nuevo" o "usado" (obligatorio).
        /// </summary>
        public required string State { get; set; } = string.Empty;

        /// <summary>
        /// Galería de imágenes del producto.
        /// </summary>
        public string []? Galery { get; set; } = [];

        /// <summary>
        /// Fecha de la última actualización del producto (obligatoria).
        /// </summary>
        public DateTime LastUpdated { get; set; } = DateTime.Now;

    }
}