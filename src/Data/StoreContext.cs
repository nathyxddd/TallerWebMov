using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TallerWebM.src.Models;

using Microsoft.EntityFrameworkCore;

namespace TallerWebM.src.Data
{
    public class StoreContext : DbContext
    {
        public class StoreContext(DbContextOptions options): base(options)
        {}

        public StoreContext(){}
        
        public required  DbSet<Product> Products {get; set;}

        public required DbSet<User> Users {get; set;}

        public required DbSet<Role> Roles = {get; set}

        public required DbSet<ShippingAddress> ShippingAddresses {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasOne(u => u.shippingAddress)
            .WithOne(s => s.User)
            .HasForeignKey<ShippingAddress>(s => s.UserId);
        }
    }

}
    
