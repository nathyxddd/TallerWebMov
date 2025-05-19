using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerWebM.src.Models;
using TallerWebM.src.Mapper;
using TallerWebMov.src.DTOs;

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
            // Se crea una instancia del modelo User y se llenan sus campos con los valores del Dto.
            var creationUser = new User
            {
                FullName = userDto.FullName,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
                Birthdate = userDto.Birthdate,
                Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                RoleId = 1
            };
            // Se devuelve el objeto User creado.
            return creationUser;
        }

        /// <summary>
        /// Método que convierte un objeto User del modelo en un objeto UserDTOResponse, para enviar los datos del usuario de forma segura al cliente.
        /// </summary>
        /// <param name="user"> El objeto User que contiene los datos del usuario. </param>
        /// <returns> Un objeto UserDTOResponse con los datos mapeados desde el User. </returns>
        public UserDTOResponse Mapper(User user)
        {
            return new UserDTOResponse
            {
                FullName = user.FullName,
                Id = user.Id,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Birthdate = user.Birthdate,
                IsActive = user.IsActive
            };
        }

    }

}