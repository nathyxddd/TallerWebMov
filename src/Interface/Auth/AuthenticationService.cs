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
using TallerWebM.src.Interfaces.Auth;


namespace TallerWebM.src.DTOs.Auth
{
    public class AuthenticationService : IAuthenticateServices
    {
        private readonly StoreContext _context;

        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        public AuthenticationService(StoreContext context)
        {
            _context = context;
        }


        public string LoginUser(string email, string password){
            Console.WriteLine("zaex");
            Console.WriteLine(email);
            Console.WriteLine("2003");
            Console.WriteLine("tatatatata" + password);
            var user = _context.Users.FirstOrDefault(u => u.Email == email);



            if(user == null)
            {
                throw new Exception("Not found");
            }
            Console.WriteLine(user.Password);
            Console.WriteLine(BCrypt.Net.BCrypt.HashPassword(password));

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.Password);

            if(!isPasswordValid)
            {
                throw new Exception("Password incorrect");
            }

            var token = GenerateToken(user);

            return token;
        }

        public string GenerateToken(User user){
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("zaex"));
            DateTime? expiration = DateTime.UtcNow.AddHours(1);
            var credentials = new SigningCredentials(
                authSigningKey,
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            return _jwtSecurityTokenHandler.WriteToken(token);
        }

        //public string RegisterUser(){

        //}
    }
}