using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using AppliancesMVC.Models;

namespace AppliancesMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<AppliancesMVC.Models.Appliance> Appliance { get; set; }
        public DbSet<AppliancesMVC.Models.Cart> Cart { get; set; }
        public DbSet<AppliancesMVC.Models.User> User { get; set; }
        public DbSet<AppliancesMVC.Models.CartAppliance> CartAppliance { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

          => optionsBuilder.UseNpgsql("Host=tyke.db.elephantsql.com;Port=5432;Database=btfayonf;Username=btfayonf;Password=YGRztec9Vh2uqHATDho4F7gia4FHXKVL;");
    }
}
