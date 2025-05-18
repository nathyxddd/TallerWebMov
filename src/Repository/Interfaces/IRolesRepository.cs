using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerWebM.src.Models;

namespace TallerWebMov.src.Repository.Interfaces
{
    public interface IRolesRepository
    {
        /// <summary>
        /// Se agrega un nuevo rol al repositorio.
        /// </summary>
        /// <param name="role"> El objeto Roles que se desea agregar. </param>
        void AddRol(Role role);

        /// <summary>
        /// Se elimina un rol del repositorio.
        /// </summary>
        /// <param name="role"> El rol que se desea eliminar. </param>
        void RemoveUser(Role role);

        /// <summary>
        /// Se busca y devuelve un rol por su ID.
        /// </summary>
        /// <param name="id"> El ID del rol a buscar. </param>
        /// <returns> El objeto Roles si se encuentra, o null si no existe. </returns>
        Role? GetRole(int id);

        /// <summary>
        /// Se guardan todos los cambios.
        /// </summary>
        void Save();
    }
}