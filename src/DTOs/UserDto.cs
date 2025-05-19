using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;

namespace TallerWebM.src.Models
{

    /// <summary>
    /// Clase DTO (Data Transfer Object) usada para transferir datos de un usuario.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Nombre completo del usuario (obligatorio).
        /// </summary>
        [Required]
        public required string FullName {get; set;} = string.Empty;

        /// <summary>
        /// Correo electrónico del usuario (obligatorio).
        /// </summary>
        [Required]
        public required string Email {get; set;} = string.Empty;

        /// <summary>
        /// Número de teléfono del usuario (obligatorio).
        /// </summary>
        [Required]
        public required string PhoneNumber {get; set;} = string.Empty;

        /// <summary>
        /// Fecha de nacimiento del usuario (obligatoria).
        /// </summary>
        [Required]
        public required string Birthdate { get; set; } = "";


        /// <summary>
        /// Contraseña del usuario (obligatoria).
        /// </summary>
        public required string Password {get; set;} = string.Empty;

        /// <summary>
        /// Contraseña repetida del usuario (obligatoria).
        /// </summary>
        public required string RepeatPassword {get; set;} = string.Empty;
    }
}