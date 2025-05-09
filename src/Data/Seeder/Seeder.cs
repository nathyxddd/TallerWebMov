using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerWebM.src.Data.Seeder
{

    /// <summary>
    /// Interfaz que define la inserción de usuarios.
    /// </summary>
    public interface IUserSeeder
    {
        /// <summary>
        /// Método para insertar datos de usuarios.
        /// </summary>
        public void Seed();
    }

    /// <summary>
    /// Interfaz que define la inserción de productos.
    /// </summary>
    public interface IProductSeeder
    {
        /// <summary>
        /// Método para insertar datos de productos.
        /// </summary>
        public void Seed();
    }
}
