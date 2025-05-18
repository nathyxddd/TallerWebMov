using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerWebM.src.Models;
using TallerWebMov.src.DTOs;

namespace TallerWebM.src
{
    /// <summary>
    /// Interfaz que define un contrato para convertir un objeto UserDto a un objeto User.
    /// </summary>
    public interface IUserCreationMapper
    {
        /// <summary>
        /// Método que toma un objeto UserDto como entrada y devuelve un objeto User, para convertir datos recibidos por el cliente.
        /// </summary>
        /// <param name="userDto"> El objeto UserDto que contiene los datos del usuario a convertir. </param>
        /// <returns> Un objeto User con los datos mapeados desde el UserDto. </returns>
        public User Mapper(UserDto userDto);

        /// <summary>
        /// Método que convierte un objeto User del modelo en un objeto UserDTOResponse, para enviar los datos del usuario de forma segura al cliente.
        /// </summary>
        /// <param name="user"> El objeto User que contiene los datos del usuario. </param>
        /// <returns> Un objeto UserDTOResponse con los datos mapeados desde el User. </returns>
        public UserDTOResponse Mapper(User user);

    }
}