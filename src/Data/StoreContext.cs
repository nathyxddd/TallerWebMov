using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerWebM.src.Models;
using Microsoft.EntityFrameworkCore;

namespace TallerWebM.src.Data
{

    /// <summary>
    /// Esta clase hereda de DbContext, que es la clase base que gestiona la conexión y el mapeo entre C# y la base de datos.
    /// </summary>
    public class StoreContext : DbContext
    {

        /// <summary>
        /// Constructor que inicializa el contexto con opciones de configuración
        /// </summary>
        /// <param name="options"> Opciones utilizadas para configurar el contexto. </param>
        public StoreContext(DbContextOptions options): base(options) {}

        /// <summary>
        /// Constructuro sin parámetros, útil en pruebas.
        /// </summary>
        public StoreContext(){}

        /// <summary>
        /// Representa la tabla "Products" en la base de datos.
        /// </summary>
        public required  DbSet<Product> Products {get; set;}

        /// <summary>
        /// Representa la tabla "Users" en la base de datos.
        /// </summary>
        public required DbSet<User> Users {get; set;}

        /// <summary>
        /// Representa la tabla "Roles" en la base de datos.
        /// </summary>
        public required DbSet<Role> Roles {get;set;}

        /// <summary>
        /// Representa la tabla "ShippingAddresses" en la base de datos.
        /// </summary>
        public required DbSet<ShippingAddress> ShippingAddresses {get; set;}


        /// <summary>
        /// Se configuran las relaciones entre las entidades del modelo.
        /// </summary>
        /// <param name="modelBuilder"> Constructor de modelos de EF Core utilizado para definir las relaciones. </param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Un usuario tiene una dirección de envío y cada dirección está asociada a un único usuario.
            modelBuilder.Entity<User>()
            .HasOne(u => u.shippingAddress)
            .WithOne(s => s.User)
            // La clave foránea está en la entidad ShippingAddress
            .HasForeignKey<ShippingAddress>(s => s.UserId);
        }
    }

}

