using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus.DataSets;

namespace TallerWebM.src.Models
{
    public class User
    {
        public required int Id {get; set;} = 0;

        public required string FullName {get; set;} = string.Empty;

        public required string Email {get; set;} = string.Empty;

        public required string PhoneNumber {get; set;} = string.Empty;

        public required DateTime Birthdate {get; set;} = DateTime.Now;

        public required string Password {get; set;} = string.Empty;

        public required ShippingAddress shippingAddress {get; set;}
    }
}