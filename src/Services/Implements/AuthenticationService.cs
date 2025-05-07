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


namespace TallerWebM.src.Services.Implements
{
    public class AuthenticationService : IAuthenticateServices
    {
        private readonly StoreContext _context;
        private readonly DbSet<User> users;
        private readonly DbSet<Role> roles;

        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        public AuthenticationService(StoreContext context)
        {
            _context = context;
            users = _context.Users;
        }


        public string LoginUser(string email, string password){

            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            var role = roles.FirstOrDefault(r => r.Id == user.Id);

            if(user == null)
            {
                throw new Exception("Not found");
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.Password);

            if(!isPasswordValid)
            {
                throw new Exception("Password incorrect");
            }

            return GenerateToken(user, id);
        }

        public string GenerateToken(User user, string role) {



            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, role)
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

        public string RegisterUser() {
            var email = userDto.Email;
            var password = userDto.Password;
            var repeatPassword = userDto.RepeatPassword;
        
           


            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if(user != null) {
                throw new Exception("user_exists");
            }

            if(password != repeatPassword) {
                throw new Exception("password_not_equals");
            }

            var creationUser = new User {
                FullName = userDto.FullName,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
                Birthdate = userDto.Birthdate,
                Password = BCrypt.Net.BCrypt.HashPassword(password)
            };

            users.Add(creationUser);
            _context.SaveChanges();
            return userDto;

        }
    }
}