using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerWebM.src.Interfaces.Auth
{
    public interface IAuthenticateServices
    {
        public string LoginUser(string email, string password);
        
    }
}