using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<LoginModel> LoginModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoginModel>().HasData(new LoginModel
            {
                Id = 1,
                UserName = "johndoe",
                Password = "def@123"
            });

            modelBuilder.Entity<Country>().HasData(
                new Country {Id=1, Country_Name = "Germany" },
                new Country {Id=2, Country_Name = "Greece" },
                new Country {Id=3, Country_Name = "England" },
                new Country {Id=4, Country_Name = "France" });

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Category_Name = "Food Supplier" },
                new Category { Id = 2,Category_Name = "Drinks Supplier" },
                new Category { Id = 3,Category_Name = "Electronic equipment" }
                );
            modelBuilder.Entity<Supplier>().HasData(
                new Supplier { Id = 1, Name = "Electronic Equipments", CategoryId = 3, TaxIdentNumber = 123456789, Address = "Koln", CountryId = 1, Email = "elEquip@gmail.com", Phone = "275698541256" },
                new Supplier { Id = 2, Name = "Food Supplies", CategoryId = 1, TaxIdentNumber = 987654321, Address = "Paris", CountryId = 4, Email = "foodEquip@gmail.com", Phone = "64789526984" },
                new Supplier { Id = 3, Name = "Drink Supplies", CategoryId = 2, TaxIdentNumber = 123789456, Address = "Athens", CountryId = 2, Email = "drinksEquip@gmail.com", Phone = "6985412566" }
                );

        }
    }
}
