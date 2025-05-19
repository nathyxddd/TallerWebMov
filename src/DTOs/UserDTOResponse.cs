using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerWebMov.src.DTOs
{
    public class UserDTOResponse
    {
        public required int Id { get; set; } = 0;
 
        /// <summary>
        /// Nombre completo del usuario (obligatorio).
        /// </summary>
        public required string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Correo electrónico del usuario (obligatorio).
        /// </summary>
        public required string Email { get; set; } = string.Empty;

        /// <summary>
        /// Número de teléfono del usuario (obligatorio).
        /// </summary>
        public required string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Fecha de nacimiento del usuario (obligatoria).
        /// </summary>
        public required bool IsActive { get; set; } = true;

        /// <summary>
        /// Fecha de nacimiento del usuario (obligatoria).
        /// </summary>
        public required string Birthdate { get; set; } = "";
    }
}