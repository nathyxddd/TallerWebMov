using System.ComponentModel.DataAnnotations;

namespace TallerWebM.src.Models {

    /// <summary>
    /// Clase que representa un rol dentro del sistema.
    /// </summary>
    public class Role {

        /// <summary>
        /// ID del rol.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre del rol, puede ser "Admin" o "User".
        /// </summary>
        public string Name { get; set; }

    }

}