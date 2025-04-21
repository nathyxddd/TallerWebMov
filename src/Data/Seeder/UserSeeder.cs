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


        public void Seed() {
            
            if(users.Any()) {
                return;
            }

            var faker = new Faker<User>()
            .RuleFor(u => u.FullName, f => f.Internet.UserName())
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.Birthdate, f => f.Date.Recent())
            .RuleFor(u => u.Password, f => BCrypt.Net.BCrypt.HashPassword(f.Internet.Password()))
            .RuleFor(u => u.PhoneNumber, f => GeneratePhoneRandom());

            faker.Generate(100).ForEach(u => {
                users.Add(u);
            });

            

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