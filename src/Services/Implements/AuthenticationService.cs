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
namespace TallerWebM.src.Services.Implements
{
    /// <summary>
    /// Implementación de la interfaz IAuthenticateServices
    /// </summary>
    public class AuthenticationService : IAuthenticateServices
    {
        // Contexto de la base de datos para acceder a los datos.
        private readonly StoreContext _context;

        // Acceso a las tabla Users.
        private readonly DbSet<User> users;

        // Acceso a las tabla Roles.
        private readonly DbSet<Role> roles;

        //
        private readonly IUserCreationMapper userCreationMappers;

        // Configuración para acceder a claves secretas.
        private readonly IConfiguration configuration;

        // Utilidad para generar y leer tokens JWT.
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();


        /// <summary>
        /// Constructor que recibe el contexto y lo asigna.
        /// </summary>
        /// <param name="context"></param>
        public AuthenticationService(StoreContext context)
        {
            _context = context;
            users = _context.Users;
        }

        // <summary>
        // Se inicia sesión de un usuario validando sus credenciales.
        // </summary>
        // <param name = "email"> El correo electrónico del usuario. </param>
        // <param name = "password"> La contraseña del usuario. </param>
        // <returns>  Token JWT si las credenciales son válidas. </returns>
        public string LoginUser(string email, string password){

            // Se busca el usuario por su email.
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            // Se obtiene el rol por el ID del usuario
            var role = roles.FirstOrDefault(r => r.Id == user.Id);

            // Si el usuario no existe, se lanza un mensaje de error.
            if(user == null)
            {
                throw new Exception("Not found");
            }

            // Se verifica si la contraseña ingresada coincide con la almacenada.
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.Password);

            // Si la contraseña no es correcta.
            if(!isPasswordValid)
            {
                throw new Exception("Password incorrect");
            }

            // Se genera el token JWT para el usuario.
            return GenerateToken(user, role.Name);
        }

        public string GenerateToken(User user, string role) {
            // Afirmaciones que se incluirán en el token.
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

        // <summary>
        // Se registra un nuevo usuario en el sistema.
        // </summary>
        // <param name = "UserDto"> Los datos del usuario a registrar. </param>
        // <returns>  El User </returns>
        public UserDto RegisterUser(UserDto userDto) {
            var email = userDto.Email;
            var password = userDto.Password;
            var repeatPassword = userDto.RepeatPassword;

            // Se verifica si el email ya está registrado.
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if(user != null) {
                throw new Exception("user_exists");
            }

            // Se verifica que ambas contraseñas coincidan.
            if(password != repeatPassword) {
                throw new Exception("password_not_equals");
            }

            // Se crea el nuevo usuario.
            var user1 = userCreationMappers.Mapper(userDto);


            // Se agrega el usuario a la base de datos.
            users.Add(user1);
            _context.SaveChanges();

            // Retorna el usuario.
            return userDto;
        }

    }
}