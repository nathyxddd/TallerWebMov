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

namespace TallerWebM.src.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]


    public class AuthenticationController(IAuthenticateServices authenticationService) : ControllerBase
    {

        [HttpPost]
        [Route("/api/login")]
        public ActionResult<string> Login([FromBody] Credentials credentials)
        {
            try{
                string token = authenticationService.LoginUser(credentials.Email, credentials.Password);
                return Ok(token);

            }catch(Exception e){
                if(e.Message == "Not found"){
                    return NotFound("No encontrado");
                }
                if(e.Message == "Password Incorrect"){
                    return Unauthorized("Contraseña incorrecta");
                }
                Console.WriteLine("Prueba");
                return BadRequest(e.Message);
                
            }

        }

        

    }
}