using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TallerWebM.src.Models;

using Microsoft.EntityFrameworkCore;

namespace TallerWebM.src.Data
{
    public class StoreContext(DbContextOptions options): DbContext(options)
    {
        public required  DbSet<Product> Products {get; set;}
    }
}