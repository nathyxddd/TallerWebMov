using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerWebM.src.Models;

namespace TallerWebM.src.Services.Interfaces.Auth
{
    // <summary>
    // Se definen los métodos necesarios para la autenticación de usuarios.
    // </summary>
    public interface IAuthenticateServices
    {
        // <summary> 
        // Se inicia sesión de un usuario validando sus credenciales.
        // </summary> 
        // <param name = "email"> El correo electrónico del usuario. </param>
        // <param name = "password"> La contraseña del usuario. </param>
        // <returns>  Token JWT si las credenciales son válidas. </returns>
        public string LoginUser(string email, string password);

        // <summary> 
        // Se registra un nuevo usuario en el sistema.
        // </summary> 
        // <param name = "UserDto"> Los datos del usuario a registrar. </param>
        // <returns>  Los datos del usuario regsitrado. </returns>
        public UserDto RegisterUser(UserDto userDto);
        
    }
}