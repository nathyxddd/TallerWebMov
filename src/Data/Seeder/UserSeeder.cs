using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Bogus;
using Microsoft.EntityFrameworkCore;
using TallerWebM.src.Models;

namespace TallerWebM.src.Data.Seeder
{
    public class UserSeeder(StoreContext storeContext) : IUserSeeder
    {

        private readonly Random _random = new Random();
        private readonly DbSet<User> users = storeContext.Users;

        private readonly DbSet<Role> roles = storeContext.Roles;


        public void Seed() {
            if(users.Any()) {
                return;
            }

            roles.Add(new Role {
              Name = "User"
            });

            roles.Add(new Role{
                Name = "Admin"
            });

            User user = new User{
                FullName = "zaex",
                Email = "zaex@gmail.com",
                PhoneNumber = "987878787",
                Birthdate = "30-03-2004",
                RoleId = 1,
                Password = BCrypt.Net.BCrypt.HashPassword("hola"),
                shippingAddress = new ShippingAddress{
                    Id = 20,
                    Street = "Antofa",
                    NumberStreet = 200,
                    Commune = "Antofagasta",
                    Region = "Antofagasta",
                    ZipCode = "3232"
                }
            };
        
            users.Add(user);

            var faker = new Faker<User>()
            .RuleFor(u => u.FullName, f => f.Internet.UserName())
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.Birthdate, f => "")
            .RuleFor(u => u.Password, f => BCrypt.Net.BCrypt.HashPassword("Nathalia123"))
            .RuleFor(u => u.PhoneNumber, f => GeneratePhoneRandom())
            .RuleFor(u => u.RoleId, 0);

            faker.Generate(100).ForEach(u => {
                users.Add(u);
            });
            storeContext.SaveChanges();
        }

        public string GeneratePhoneRandom(){
            string phone = "9";

            for (int i = 0; i< 8; i++){
                string s =_random.NextInt64(0,9) + "";
                phone += s;
            }
            return phone;
        }
    }
}