using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerWebM.src.Models;

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

    }
}