
using Microsoft.EntityFrameworkCore;
using TechStore.interfaces;
using TechStore.Models;

namespace TechStore.Repository{
    public class OrderRepo : IAllOrders
    {

        private readonly AppDBContent appDBContent;
        private readonly Cart cart;

        public OrderRepo(AppDBContent appDBContent, Cart cart) {
            this.appDBContent = appDBContent;
            this.cart = cart;
        }

        public void CreateOrder(Order order)
        {
            order.OrderTime = DateTime.Now;
            appDBContent.Order.Add(order);

            var items = cart.ListCartItems;

            foreach(var el in items) {
                var orderDetails = new OrderDetail() {
                    ProductId = el.Product.Id,
                    OrderId = order.Id,
                    Price = el.Product.Price
                };
                appDBContent.OrderDetail.Add(orderDetails);
            }
            appDBContent.SaveChanges();
        }
    }
}