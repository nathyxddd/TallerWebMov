using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TallerWebM.src.Models
{

    /// <summary>
    /// Clase que representa a un usuario.
    /// </summary>
    public class User
    {
        /// <summary>
        /// ID del usuario (por defecto 0).
        /// </summary>
        public int Id {get; set;} = 0;

        /// <summary>
        /// Nombre completo del usuario (obligatorio).
        /// </summary>
        public required string FullName {get; set;} = string.Empty;

        /// <summary>
        /// Correo electrónico del usuario (obligatorio).
        /// </summary>
        public required string Email {get; set;} = string.Empty;

        /// <summary>
        /// Número de teléfono del usuario (obligatorio).
        /// </summary>
        public required string PhoneNumber {get; set;} = string.Empty;

        /// <summary>
        /// Fecha de nacimiento del usuario (obligatoria).
        /// </summary>
        public required string Birthdate { get; set; } = "";

        /// <summary>
        /// Contraseña del usuario (obligatoria).
        /// </summary>
        public required string Password {get; set;} = string.Empty;
        
        /// <summary>
        /// ID del rol del usuario (por defecto 0).
        /// </summary>
        public int RoleId { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Dirección de envío asociada al usuario.
        /// </summary>
        public ShippingAddress shippingAddress {get; set;}
    }
}