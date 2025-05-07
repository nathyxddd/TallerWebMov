using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;

namespace TallerWebM.src.Models
{
    public class UserDto
    {
        public required string FullName {get; set;} = string.Empty;

        public required string Email {get; set;} = string.Empty;

        public required string PhoneNumber {get; set;} = string.Empty;

        public required string Birthdate {get; set;} = string.Empty;

        public required string Password {get; set;} = string.Empty;  
    }
}