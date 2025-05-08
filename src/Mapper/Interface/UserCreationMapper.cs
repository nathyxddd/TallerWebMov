using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerWebM.src.Models;

namespace TallerWebM.src.Mapper
{

    /// <summary>
    /// Clase que implementa la interfaz IUserCreationMapper, convirtiendo objetos a tipo UserDto a User.
    /// </summary>
    public class UserCreationMapper : IUserCreationMapper
    {
        
        /// <summary>
        /// Implementación del método Mapper que convierte un UserDto en un User.
        /// </summary>
        /// <param name="userDto"> El objeto UserDto que contiene los datos del usuario a convertir. </param>
        /// <returns> Un objeto User con los datos mapeados desde el UserDto. </returns>
        public User Mapper(UserDto userDto)
        {
            
        }

    }

}