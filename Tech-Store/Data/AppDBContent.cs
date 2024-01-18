using Microsoft.EntityFrameworkCore;
using TechStore.Models;

namespace TechStore{
    public class AppDBContent : DbContext{
        public AppDBContent(DbContextOptions<AppDBContent> options) : base(options) {

        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Models.Type> Type { get; set; }
        public DbSet<CartItem> CartItem { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        }
}