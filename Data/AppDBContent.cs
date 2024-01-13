// using System;
// using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
// using System.Linq;
// using System.Threading.Tasks;
using TechStore.Models;

namespace TechStore{
    public class AppDBContent : DbContext{
        public AppDBContent(DbContextOptions<AppDBContent> options) : base(options) {

        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Models.Type> Type { get; set; }
        public DbSet<CartItem> CartItem { get; set; }
        }
}