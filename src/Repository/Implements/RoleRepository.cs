using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TallerWebM.src.Data;
using TallerWebM.src.Models;
using TallerWebM.src.Services.Interface;
using TallerWebMov.src.Repository.Interfaces;

namespace TallerWebMov.src.Repository.Implements
{
    /// <summary>
    /// Implementación del repositorio para la entidad Role, permite realizar operaciones CRUD sobre la tabla roles.
    /// </summary>
    public class RoleRepository : IRolesRepository
    {
        /// <summary>
        /// Tabla de roles.
        /// </summary>
        private readonly DbSet<Role> roles;

        /// <summary>
        /// Contexto de la base de datos.
        /// </summary>
        private readonly StoreContext storeContext;

        /// <summary>
        /// Constructor del repositorio.
        /// </summary>
        /// <param name="storeContext"> El contexto de la base de datos. </param>
        public RoleRepository(StoreContext storeContext)
        {
            this.storeContext = storeContext;
            roles = storeContext.Roles;
        }

        /// <summary>
        /// Se agrega un nuevo rol al repositorio.
        /// </summary>
        /// <param name="role"> El objeto Roles que se desea agregar. </param>
        public void AddRol(Role role)
        {
            roles.Add(role);
        }

        /// <summary>
        /// Se busca y devuelve un rol por su ID.
        /// </summary>
        /// <param name="id"> El ID del rol a buscar. </param>
        /// <returns> El objeto Roles si se encuentra, o null si no existe. </returns>
        public Role? GetRole(int id)
        {
            return roles.Find(id);
        }

        /// <summary>
        /// Se elimina un rol del repositorio.
        /// </summary>
        /// <param name="role"> El rol que se desea eliminar. </param>
        public void RemoveUser(Role role)
        {
            roles.Remove(role);
        }

        /// <summary>
        /// Se guardan todos los cambios.
        /// </summary>
        public void Save()
        {
            storeContext.SaveChanges();
        }
    }
}