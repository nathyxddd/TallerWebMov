using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TallerWebM.src.Data;
using TallerWebM.src.Models;
using TallerWebMov.src.Repository.Interfaces;


namespace TallerWebMov.src.Repository.Implements
{
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// Tabla de usuarios.
        /// </summary>
        private readonly DbSet<User> users;

        /// <summary>
        /// Contexto de la base de datos utilizado para guardar los cambios.
        /// </summary>
        private readonly StoreContext storeContext;

        /// <summary>
        /// Constructor del repositorio.
        /// </summary>
        /// <param name="storeContext"> COntexto de la base de datos. </param>
        public UserRepository(StoreContext storeContext)
        {
            this.storeContext = storeContext;
            this.users = storeContext.Users;
        }

        /// <summary>
        /// Se agrega un nuevo usuario al repositorio.
        /// </summary>
        /// <param name="user"> El objeto de tipo User a agregar. </param>
        public void AddUser(User user)
        {
            users.Add(user);
        }

        /// <summary>
        /// Se obtiene un usuario a aprtir de su ID.
        /// </summary>
        /// <param name="id"> El ID del usuario. </param>
        /// <returns> El objeto User si se encuentra, o null si no existe. </returns>
        public User? GetUser(int id)
        {
            return users.Find(id);
        }

        /// <summary>
        /// Se obtiene un usuario a partir de su correo electrónico.
        /// </summary>
        /// <param name="email"> El correo electrónico del usuario a buscar.</param>
        /// <returns> El objeto User si se encuentra, o null si no existe. </returns>
        public User? GetUserEmail(string email)
        {
            return users.FirstOrDefault(u => u.Email == email);
        }

        /// <summary>
        /// Se elimina un usuario del repositorio.
        /// </summary>
        /// <param name="user"> Usuario a eliminar. </param>
        public void RemoveUser(User user)
        {
            users.Remove(user);
        }

        /// <summary>
        /// Se guardan los cambios realizados en el repositorio.
        /// </summary>
        public void Save()
        {
            storeContext.SaveChanges();
        }

        /// <summary>
        /// Se actualiza los datos de un usuario existente en el repositorio. 
        /// </summary>
        /// <param name="user"> El usuario con los datos modificados. </param>
        public void UpdateUser(User user)
        {
            users.Update(user);
        }
    }
}