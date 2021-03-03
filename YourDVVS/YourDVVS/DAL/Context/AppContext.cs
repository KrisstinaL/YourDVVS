using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourDVVS.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace YourDVVS.DAL.Context
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }
    }
}
