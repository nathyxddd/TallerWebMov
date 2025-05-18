using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerWebM.src.Models;
using TallerWebMov.src.DTOs;

namespace TallerWebM.src.Services.Interfaces.Auth
{
    /// <summary>
    /// Se definen los métodos necesarios para la autenticación de usuarios.
    /// </summary>
    public interface IAuthenticateServices
    {
        /// <summary>
        /// Se inicia sesión de un usuario validando sus credenciales.
        /// </summary>
        /// <param name = "email"> El correo electrónico del usuario. </param>
        /// <param name = "password"> La contraseña del usuario. </param>
        /// <returns>  Token JWT si las credenciales son válidas. </returns>
        public string LoginUser(string email, string password);

        /// <summary>
        /// Se registra un nuevo usuario en el sistema.
        /// </summary>
        /// <param name = "UserDto"> Los datos del usuario a registrar. </param>
        /// <returns>  Los datos del usuario regsitrado. </returns>
        public UserDTOResponse RegisterUser(UserDto userDto);

        /// <summary>
        /// Se actualiza el estado de un usuario por su ID.
        /// </summary>
        /// <param name="userId"> El ID del usuario cuyo estado se actualizará. </param>
        /// <param name="status"> Nuevo estado del usuario (true para habilitado, false para deshabilitado). </param>
        public void UpdateStatus(int userId, bool status);

        /// <summary>
        /// Se desactiva la cuenta de un usuario.
        /// </summary>
        /// <param name="userId"> El ID del usuario a desactivar. </param>
        /// <returns> El objeto con los datos del usuario desactivado. </returns>
        public UserDTOResponse DisableAccount(int userId);

        /// <summary>
        /// Se activa la cuenta de un usuario previamente desactivado.
        /// </summary>
        /// <param name="userId"> El ID del usuario a activar. </param>
        /// <returns> El objeto con los datos del usuario activado. </returns>
        public UserDTOResponse EnableAccount(int userId);

        /// <summary>
        /// Se obtienen los datos de un usuario por su ID.
        /// </summary>
        /// <param name="userId"> El ID del usuario a obtener. </param>
        /// <returns> El objeto con los datos del usuario. </returns>
        public UserDTOResponse GetUser(int userId);

        /// <summary>
        /// Se buscan usuarios filtrando por estado, fechas, correo electrónico y nombre.
        /// </summary>
        /// <param name="state"> Estado del usuario. </param>
        /// <param name="firstDate"> Fecha inicial del rango de búsqueda. </param>
        /// <param name="secondDate"> Fecha final del rango de búsqueda. </param>
        /// <param name="email"> Correo electrónico del usuario. </param>
        /// <param name="name"> Nombre del usuario. </param>
        /// <returns> Lista de usuarios que cumplen con los criterios de búsqueda.</returns>
        public List<UserDTOResponse> Search(bool? state, string? firstDate, string? secondDate, string? email, string? name);

    }
}