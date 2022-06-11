using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Appliances.Models;

namespace Appliances.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Appliances.Models.Appliance> Appliance { get; set; }
        public DbSet<Appliances.Models.Cart> Cart { get; set; }
        public DbSet<Appliances.Models.User> User { get; set; }
        public DbSet<Appliances.Models.CartAppliance> CartAppliance { get; set; }
    }
}
