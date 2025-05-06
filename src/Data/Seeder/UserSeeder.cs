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
            Console.WriteLine("ooooo");
            if(users.Any()) {
                return;
            }
            Console.WriteLine("oou");

            //var faker = new Faker<User>()
          //  .RuleFor(u => u.FullName, f => f.Internet.UserName())
        //    .RuleFor(u => u.Email, f => f.Internet.Email())
      //      .RuleFor(u => u.Birthdate, f => f.Date.Recent())
    //        .RuleFor(u => u.Password, f => BCrypt.Net.BCrypt.HashPassword("2003"))
  //          .RuleFor(u => u.PhoneNumber, f => GeneratePhoneRandom());
//

            //faker.Generate(100).ForEach(u => {
              //  users.Add(u);
            //});


            string pep = BCrypt.Net.BCrypt.HashPassword("2003");
            Console.WriteLine(pep);

            User user = new User{
                FullName = "zaex",
                Email = "zaex@gmail.com",
                PhoneNumber = "987878787",
                Birthdate = new DateTime(),
                Password = pep,
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
            storeContext.SaveChanges();
            Console.WriteLine("pppp");

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