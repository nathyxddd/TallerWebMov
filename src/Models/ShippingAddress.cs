using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerWebM.src.Models
{
    /// <summary>
    /// Clase que representa una dirección de envío de un usuario.
    /// </summary>
    public class ShippingAddress
    {
        /// <summary>
        /// ID de la dirección de envío (obligatoria).
        /// </summary>
        public required int Id { get; set; } = 0;

        /// <summary>
        /// Calle de la dirección (obligatoria).
        /// </summary>
        public required string Street { get; set; } = string.Empty;


        /// <summary>
        /// Núumero de la calle (por defecto 0) (obligatorio).
        /// </summary>
        public required int NumberStreet { get; set; } = 0;


        /// <summary>
        /// Comuna o ciudad de la dirección (obligatoria).
        /// </summary>
        public required string Commune { get; set; } = string.Empty;


        /// <summary>
        /// Región de la dirección (obligatoria).
        /// </summary>
        public required string Region { get; set; } = string.Empty;


        /// <summary>
        /// Código postal de la dirección (obligatorio).
        /// </summary>
        public required string ZipCode { get; set; } = string.Empty;

        /// <summary>
        /// ID del uduario al que pertenece esta dirección.
        /// </summary>
        public int UserId { get; set; } = 0;

        /// <summary>
        /// Relación con el usuario, que permite acceder al objeto "User" relacionado con esta dirección.
        /// </summary>
        public User User { get; set; } 
        }
}