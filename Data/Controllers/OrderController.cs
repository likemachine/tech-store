using Microsoft.AspNetCore.Mvc;
using TechStore.interfaces;
using TechStore.Models;

namespace TechStore.Controllers {
    public class OrderController : Controller {
        private readonly IAllOrders allOrders;
        private readonly Cart cart;

        public OrderController(IAllOrders allOrders, Cart cart) {
            this.allOrders = allOrders;
            this.cart = cart;
        }

        [Route("Order/Checkout")]
        public IActionResult Checkout() {
            return View();
        }
    }
}