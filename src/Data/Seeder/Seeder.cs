using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerWebM.src.Data.Seeder
{
    public interface IUserSeeder
    {
        public void Seed();
    }

    public interface IProductSeeder
    {
        public void Seed();
    }
}
