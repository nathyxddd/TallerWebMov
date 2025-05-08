using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace TallerWebM.src.DTOs
{
    
    /// <summary>
    /// Clase DTO (Data Transfer Object) usada para transferir datos de inicio de sesión.
    /// </summary>
    public class Credentials
    {
        
        /// <summary>
        /// Almacena el correo electrónico del usuario.
        /// </summary>
        public string Email {get; set;}

        /// <summary>
        /// Almacena la contraseña del usuario.
        /// </summary>
        public string Password {get; set;}

    }
}