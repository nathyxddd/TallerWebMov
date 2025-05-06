using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TallerWebM.src.Models
{
    public class User
    {
        public int Id {get; set;} = 0;

        public required string FullName {get; set;} = string.Empty;

        public required string Email {get; set;} = string.Empty;

        public required string PhoneNumber {get; set;} = string.Empty;

        public required DateTime Birthdate {get; set;} = DateTime.Now;

        public required string Password {get; set;} = string.Empty;

        public ShippingAddress shippingAddress {get; set;}
    }
}