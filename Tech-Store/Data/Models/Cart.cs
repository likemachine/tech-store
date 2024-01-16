// using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.DependencyInjection;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Tech_Store.Migrations;

namespace TechStore.Models {
    public class Cart {
        private readonly AppDBContent appDBContent;
        public Cart (AppDBContent appDBContent){
            this.appDBContent = appDBContent;
        }
        public string CartId { get; set; }

        public List<CartItem> ListCartItems { get; set; }

        public static Cart GetCart(IServiceProvider services) {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDBContent>();
            string CartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", CartId);

            return new Cart(context) { CartId = CartId };
        }

        public void AddToCart(Product product) {
            this.appDBContent.CartItem.Add(new CartItem {
                CartId = CartId,
                Product = product,
                Price = product.Price,
            });

            appDBContent.SaveChanges();
        }

        public List<CartItem> getCartItems() {
            return appDBContent.CartItem.Where(c => c.CartId == CartId).Include(s => s.Product).ToList();
        }
    }
}