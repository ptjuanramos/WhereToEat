using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhereToEat.Data.Models;

namespace WhereToEat.Data
{
    public class WhereToEatContext : DbContext
    {
        public DbContextOptions<WhereToEatContext> Options { get; private set; }

        public WhereToEatContext(DbContextOptions<WhereToEatContext> options) : base(options)
        {
            Options = options;
        }

        public DbSet<User> Users { get; set; }

        public DbSet<User> Groups { get; set; }
    }
}
