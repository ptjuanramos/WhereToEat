using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhereToEat.Data.Models;
using WhereToEat.Web.Helpers;

namespace WhereToEat
{
    public class WhereToEatContext : DbContext
    {
        public DbContextOptions<WhereToEatContext> Options { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();

            byte[] salt = EncryptionHelper.GenerateSalt();
            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = new Guid("0D28FEAD-09FC-462A-93B1-A6231342533F"),
                    Email = "ptjuanramos@gmail.com",
                    Password = EncryptionHelper.HashPassword("123456", salt),
                    Salt = Convert.ToBase64String(salt),
                    Username = "ptjuanramos@gmail.com"
                });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
#if DEBUG
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
#else
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ProductionConnection")); //Changes to production db
#endif
        }

        public WhereToEatContext() { }

        public DbSet<User> Users { get; set; }

        public DbSet<Group> Groups { get; set; }
    }
}
