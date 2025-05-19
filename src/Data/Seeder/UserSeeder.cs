using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Bogus;
using Bogus.DataSets;
using Microsoft.EntityFrameworkCore;
using TallerWebM.src.Models;

namespace TallerWebM.src.Data.Seeder
{

    /// <summary>
    /// Clase que implementa la interfaz IUserSeeder para insertar usuarios de prueba.
    /// </summary>
    public class UserSeeder(StoreContext storeContext) : IUserSeeder
    {
        // Generador de números aleatorios.
        private readonly Random _random = new Random();

        // Acceso a la tabla de usuarios.
        private readonly DbSet<User> users = storeContext.Users;

        // Acceso a la tabla de roles.
        private readonly DbSet<Role> roles = storeContext.Roles;

        /// <summary>
        /// Método principal que inserta datos de prueba relacionados con usuarios y roles en la base de datos.
        /// </summary>
        public void Seed() {
            // Si ya existen usuarios en la base de datos, no se hace nada.
            if(users.Any()) {
                return;
            }

            // Se agrega el rol "User"
            roles.Add(new Role {
              Name = "User"
            });

            // Se agrega el rol "Admin"
            roles.Add(new Role{
                Name = "Admin"
            });

            // Se crea un usuario específico de forma manual.
            User user = new User{
                FullName = "Ignacio Mancilla",
                Email = "ignacio.mancilla@gmail.com",
                PhoneNumber = "03/03/2003",
                Birthdate = "",
                RoleId = 2,
                Password = BCrypt.Net.BCrypt.HashPassword("Pa$$word2025"),
                shippingAddress = new ShippingAddress{
                    Id = 20,
                    Street = "Antofa",
                    NumberStreet = 200,
                    Commune = "Antofagasta",
                    Region = "Antofagasta",
                    ZipCode = "3232"
                }
            };

            // Se agrega ese usuario a la base de datos.
            users.Add(user);

            // Se crea un generador de usuarios falsos usando Bogus.
            var faker = new Faker<User>()
            // Nombre de usuario.
            .RuleFor(u => u.FullName, f => f.Internet.UserName())
            // Email falso.
            .RuleFor(u => u.Email, f => f.Internet.Email())
            // Fecha de nacimiento (actual por ahora).
            .RuleFor(u => u.Birthdate, f => "")
            // Contraseña encriptada.
            .RuleFor(u => u.Password, f => BCrypt.Net.BCrypt.HashPassword("Nathalia123"))
            // Número de teléfono aleatorio.
            .RuleFor(u => u.PhoneNumber, f => GeneratePhoneRandom())
            // Id 0, por ahora.
            .RuleFor(u => u.RoleId, 1);

            // Se generan 100 usuarios falsos y se agregan a la base de datos.
            faker.Generate(100).ForEach(u => {
                users.Add(u);
            });

            // Se guardan los cambios en la base de datos.
            storeContext.SaveChanges();
        }


        /// <summary>
        /// Método para generar un número de teléfono chileno aleatorio que comienza con 9 y sigue con 8 dígitos.
        /// </summary>
        /// <returns> Número de teléfono como string. </returns>
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