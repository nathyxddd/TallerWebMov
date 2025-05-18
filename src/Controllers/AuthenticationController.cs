using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TallerWebM.src.Models;
using TallerWebM.src.Data;
using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using TallerWebM.src.DTOs;
using TallerWebM.src.Services.Interfaces.Auth;
using TallerWebMov.src.DTOs;

namespace TallerWebM.src.Controllers
{

    // Se define la ruta basa del controlador.
    [Route("api/[controller]")]
    //Responde a las solicitudes API comportandose como un controlador REST.
    [ApiController]

    ///<summary>
    /// Controlador del Authentication, encargado de manejar la autenticación de usuarios, implementa la interfaz IAuthenticate Services.
    /// </summary>
    public class AuthenticationController(IAuthenticateServices authenticationService) : ControllerBase
    {

        /// <summary
        /// Método HTTP POST para que un usuario pueda iniciar sesión utilizando su correo y contraseña.
        /// </summary>
        /// <param name="credentials"> Objeto que contiene el correo electrónico y la contraseña del usuario. </param> 
        /// <returns> Devuelve un token JWT si la autenticación es exitosa.
        /// Retorna un cógido 404 si el usuario no existe.
        /// Retorna un código 401 si la contraseña es incorrecta.
        /// Retorna un código 400 si ocurre otro error inesperado.
        /// </return>
        [HttpPost]
        [Route("/api/login")]
        public ActionResult<string> Login([FromBody] Credentials credentials)
        {
            try
            {
                // Se intenta autenticar al usuario con el correo y contraseña enviados en el cuerpo de la solicitud.
                string token = authenticationService.LoginUser(credentials.Email, credentials.Password);

                // Si la autenticación fue exitosa, se devuelve un token con código 200: la solicitud ha sido procesada correctamente.
                return Ok(token);

            }
            catch (Exception e)
            {
                // Si el servicio lanza una excepción con mensaje "Not found", significa que el usuario no existe.
                if (e.Message == "Not found")
                {
                    // Se devuelve el código 404: Recurso no encontrado
                    return NotFound("No encontrado");
                }

                // Si el mensaje es "Password Incorrect", la contraseña no coinciden con la del usuario.
                if (e.Message == "Password Incorrect")
                {
                    // Se devuelve el código 401: No autorizado por información incorrecta
                    return Unauthorized("Contraseña incorrecta");
                }
                Console.WriteLine("Prueba");

                // Se devuelve un mensaje de error 400: Solicitud incorrecta
                return BadRequest(e.Message);

            }

        }


        /// <summary>
        /// Método HTTP POST para que un usuario se pueda registrar a través de una solicutud.
        /// </summary>
        /// <param name="userDto"> Objeto que contiene los datos del usuario a registrar. </param>
        /// <returns> El código 200 si el registro fue exitoso. </returns>
        [HttpPost]
        [Route("/api/register")]
        public ActionResult<UserDto> Register([FromBody] UserDto userDto)
        {
            try
            {
                // Intenta registrar al usuario utilizando el servicio de autenticación.
                var user = authenticationService.RegisterUser(userDto);

                // Devuelve una respuesta HTTP 200 con los datos del usuario registrado.
                return Ok(user);

            }
            catch (Exception e)
            {

                if (e.Message == "user_exists")
                {
                    return NotFound("Usuario ya existente.");
                }
                if (e.Message == "password_not_equals")
                {
                    return Unauthorized("Las claves no son iguales");
                }
                return BadRequest(e.Message);
            }
        }
        
        /// <summary>
        /// Método HTTP PATCH para deshabilitar una cuenta de usuario.
        /// </summary>
        /// <param name="id"> El ID del usuario a deshabilitar. </param>
        /// <returns> Devuelve el usuario actualizado si la operación es exitosa, en caso contrario un error 404 si el usuario no se encuentra. </returns>
        [HttpPatch]
        [Route("/api/disable-user/{id}")]
        public ActionResult<UserDTOResponse> DisableUser(int id)
        {
            try
            {
                return Ok(authenticationService.DisableAccount(id));
            }
            catch (Exception)
            {
                return NotFound("User not found");
            }
        }

        /// <summary>
        /// Método HTTP PATCH para habilitar una cuenta de usuario.
        /// </summary>
        /// <param name="id"> El ID del usuario a habilitar. </param>
        /// <returns> Devuelve el usuario actualizado si la operación es exitosa, en caso contrario un error 404 si el usuario no se encuentra. </returns>
        [HttpPatch]
        [Route("/api/enable-user/{id}")]
        public ActionResult<UserDTOResponse> EnableUser(int id)
        {
            try
            {
                return Ok(authenticationService.EnableAccount(id));
            }
            catch (Exception)
            {
                return NotFound("User not found");
            }
        }

        /// <summary>
        /// Método HTTP GET para buscar usuarios según distintos parametros. 
        /// </summary>
        /// <param name="state"> El estado del usuario. </param>
        /// <param name="firstDate"> La fecha de inicio del rango de búsqueda. </param>
        /// <param name="secondDate"> La fecha de fin del rango de búsqueda. </param>
        /// <param name="email"> El correo electrónico del usuario. </param>
        /// <param name="name"> El nombre del usuario. </param>
        /// <returns> Una lista de usuarios que cumplen con los filtros especificados. </returns>
        [HttpGet]
        [Route("/api/search")]
        public ActionResult<List<UserDTOResponse>> Search( [FromQuery] bool? state, [FromQuery]  string? firstDate, [FromQuery]  string? secondDate,  [FromQuery]  string? email, [FromQuery]  string? name)
        {
            return authenticationService.Search(state, firstDate, secondDate, email, name);
        }

    }
}