using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerWebM.src.Data;
using BCrypt.Net;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TallerWebM.src.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication;
using TallerWebM.src.Services.Interfaces.Auth;
using Microsoft.EntityFrameworkCore;
using TallerWebM.src.Mapper;
using System.Data;
using TallerWebMov.src.Repository.Interfaces;
using TallerWebMov.src.DTOs;
namespace TallerWebM.src.Services.Implements
{
    /// <summary>
    /// Implementación de la interfaz IAuthenticateServices.
    /// </summary>
    public class AuthenticationService : IAuthenticateServices
    {
        /// <summary>
        /// Referencia a los usuarios en la base de datos.
        /// </summary>
        private readonly DbSet<User> users;

        /// <summary>
        /// Repositorio de roles para obtener roles de usuarios.
        /// </summary>
        private readonly IRolesRepository rolesRepository;

        /// <summary>
        /// Repositorio de usuarios para operaciones con datos de usuarios.
        /// </summary>
        private readonly IUserRepository userRepository;

        /// <summary>
        /// Mapper para convertir entre entidades y DTOs de usuarios.
        /// </summary>
        private readonly IUserCreationMapper userCreationMappers;

        /// <summary>
        /// Configuración de la aplicación (para acceder a claves como la secreta del token).
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// Manejador para generar tokens JWT.
        /// </summary>
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();


        /// <summary>
        /// Constructor que recibe el contexto y lo asigna.
        /// </summary>
        /// <param name="rolesRepository"></param>
        /// <param name="userRepository"></param>
        /// <param name="storeContext"></param>
        public AuthenticationService(IRolesRepository rolesRepository,
        IUserRepository userRepository,
        StoreContext storeContext)
        {
            this.rolesRepository = rolesRepository;
            this.userRepository = userRepository;
            users = storeContext.Users;
        }

        /// <summary>
        /// Se inicia sesión de un usuario validando sus credenciales.
        /// </summary>
        /// <param name="email"> El correo electrónico del usuario. </param>
        /// <param name="password"> La contraseña del usuario. </param>
        /// <returns> Token JWT si las credenciales son válidas. </returns>
        /// <exception cref="Exception"></exception>
        public string LoginUser(string email, string password)
        {

            // Se busca el usuario por su email.
            var user = userRepository.GetUserEmail(email);

            // Se obtiene el rol por el ID del usuario.

            // Si el usuario no existe, se lanza un mensaje de error.
            if (user == null)
            {
                throw new Exception("Not found");
            }

            // Se obtiene el rol del usuario.
            var role = rolesRepository.GetRole(user.Id);
            if (role == null)
            {
                throw new Exception("Role Not Found");
            }

            // Se verifica si la contraseña ingresada coincide con la almacenada.
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.Password);

            // Si la contraseña no es correcta.
            if (!isPasswordValid)
            {
                throw new Exception("Password incorrect");
            }

            // Se genera el token JWT para el usuario.
            return GenerateToken(user, role.Name);
        }
        
        /// <summary>
        /// Genera un token JWT para el usuario autenticado.
        /// </summary>
        /// <param name="user"> El usuario autenticado. </param>
        /// <param name="role"> El rol del usuario. </param>
        /// <returns> Un string que representa el token JWT generado. </returns>
        public string GenerateToken(User user, string role)
        {
            // Lista de claims (información codificada en el token).
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, role)
            };

            // Se obtiene la clave secreta desde appsettings.json.
            var contentToken = configuration["Jwt:Secret"];
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(contentToken));

            // Tiempo de expiración del token.
            DateTime? expiration = DateTime.UtcNow.AddHours(1);

            // Se firma el token con algoritmo HMAC-SHA256.
            var credentials = new SigningCredentials(
                authSigningKey,
                SecurityAlgorithms.HmacSha256
            );

            // Se genera el token con los claims y la firma.
            var token = new JwtSecurityToken(
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            // Se retorna el token generado en formato string.
            return _jwtSecurityTokenHandler.WriteToken(token);
        }

        
        /// <summary>
        /// Se registra un nuevo usuario en el sistema.
        /// </summary>
        /// <param name="userDto"> Los datos del usuario a registrar. </param>
        /// <returns> El User </returns>
        /// <exception cref="Exception"> Lanza excepciones si el usuario ya existe o las contraseñas no coinciden. </exception>
        public UserDTOResponse RegisterUser(UserDto userDto)
        {
            var email = userDto.Email;
            var password = userDto.Password;
            var repeatPassword = userDto.RepeatPassword;

            // Se verifica si el email ya está registrado.
            var user = userRepository.GetUserEmail(email);
            if (user != null)
            {
                throw new Exception("user_exists");
            }

            // Se verifica que ambas contraseñas coincidan.
            if (password != repeatPassword)
            {
                throw new Exception("password_not_equals");
            }

            // Mapea el DTO a una entidad User.
            var user1 = userCreationMappers.Mapper(userDto);


            // Se agrega y guarda el nuevo usuario.

            userRepository.AddUser(user1);
            userRepository.Save();

            // Retorna el DTO del usuario registrado.
            return userCreationMappers.Mapper(user1);
        }

        /// <summary>
        /// Se actualiza el estado de un usuario por su ID.
        /// </summary>
        /// <param name="userId"> El ID del usuario cuyo estado se actualizará. </param>
        /// <param name="status"> Nuevo estado del usuario (true para habilitado, false para deshabilitado). </param>
        public void UpdateStatus(int userId, bool status)
        {

            var user = userRepository.GetUser(userId);
            if (user == null)
            {
                return;
            }

            user.IsActive = status;
            userRepository.UpdateUser(user);
        }

        /// <summary>
        /// Se desactiva la cuenta de un usuario.
        /// </summary>
        /// <param name="userId"> El ID del usuario a desactivar. </param>
        /// <returns> El objeto con los datos del usuario desactivado. </returns>
        /// <exception cref="Exception"> Lanza excepción si el usuario no existe. </exception>
        public UserDTOResponse DisableAccount(int userId)
        {
            var user = userRepository.GetUser(userId);
            if (user == null)
            {
                throw new Exception("not_found");
            }

            user.IsActive = false;
            userRepository.UpdateUser(user);
            return userCreationMappers.Mapper(user);
        }

        /// <summary>
        /// Se activa la cuenta de un usuario previamente desactivado.
        /// </summary>
        /// <param name="userId"> El ID del usuario a activar. </param>
        /// <returns> El objeto con los datos del usuario activado. </returns>
        /// <exception cref="Exception"> Lanza excepción si el usuario no existe. </exception>
        public UserDTOResponse EnableAccount(int userId)
        {
            var user = userRepository.GetUser(userId);
            if (user == null)
            {
                throw new Exception("not_found");
            }

            user.IsActive = true;
            userRepository.UpdateUser(user);
            return userCreationMappers.Mapper(user);
        }
        
        /// <summary>
        /// Se obtienen los datos de un usuario por su ID.
        /// </summary>
        /// <param name="userId"> El ID del usuario a obtener. </param>
        /// <returns> El objeto con los datos del usuario. </returns>
        /// <exception cref="NotImplementedException"> Esta funcionalidad aún no está implementada. </exception>
        public UserDTOResponse GetUser(int userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Se buscan usuarios filtrando por estado, fechas, correo electrónico y nombre.
        /// </summary>
        /// <param name="state"> Estado del usuario. </param>
        /// <param name="firstDate"> Fecha inicial del rango de búsqueda. </param>
        /// <param name="secondDate"> Fecha final del rango de búsqueda. </param>
        /// <param name="email"> Correo electrónico del usuario. </param>
        /// <param name="name"> Nombre del usuario. </param>
        /// <returns> Lista de usuarios que cumplen con los criterios de búsqueda.</returns>
        public List<UserDTOResponse> Search(bool? state, string? firstDate, string? secondDate, string? email, string? name)
        {
            // Se emppieza con todos los usuarios.
            IEnumerable<User> search = users;

            // Aplica el filtro por estado
            if (state != null)
            {
                search = search.Where(u => u.IsActive == state);
            }

            // Aplica el filtro por email

            if (email != null)
            {
                search = search.Where(u => u.Email == email);
            }

            // Aplica el filtro por nombre
            if (name != null)
            {
                search = search.Where(u => u.FullName == name);
            }


            return null;
        }

    }
}