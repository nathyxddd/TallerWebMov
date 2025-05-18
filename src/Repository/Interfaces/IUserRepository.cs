using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerWebM.src.Models;

namespace TallerWebMov.src.Repository.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Se agrega un nuevo usuario al repositorio.
        /// </summary>
        /// <param name="user"> El objeto de tipo User a agregar. </param>
        void AddUser(User user);

        /// <summary>
        /// Se actualiza los datos de un usuario existente en el repositorio. 
        /// </summary>
        /// <param name="user"> El usuario con los datos modificados. </param>
        void UpdateUser(User user);

        /// <summary>
        /// Se elimina un usuario del repositorio.
        /// </summary>
        /// <param name="user"> Usuario a eliminar. </param>
        void RemoveUser(User user);

        /// <summary>
        /// Se obtiene un usuario a partir de su correo electrónico.
        /// </summary>
        /// <param name="email"> El correo electrónico del usuario a buscar.</param>
        /// <returns> El objeto User si se encuentra, o null si no existe. </returns>
        User? GetUserEmail(string email);

        /// <summary>
        /// Se obtiene un usuario a aprtir de su ID.
        /// </summary>
        /// <param name="id"> El ID del usuario. </param>
        /// <returns> El objeto User si se encuentra, o null si no existe. </returns>
        User? GetUser(int id);

        /// <summary>
        /// Se guardan los cambios realizados en el repositorio.
        /// </summary>
        void Save();
    }
}