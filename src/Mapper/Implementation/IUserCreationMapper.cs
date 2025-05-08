using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerWebM.src.Models;

namespace TallerWebM.src
{
    public interface IUserCreationMapper
    {

        public User Mapper(UserDto userDto);

    }

}