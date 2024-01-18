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
        public IActionResult Checkout() 
        {
            return View();
        }

        [HttpPost("Order/Checkout")]
        public IActionResult Checkout(Order order) {
            
            cart.ListCartItems = cart.getCartItems();
            
            if(cart.ListCartItems.Count == 0) {
                ModelState.AddModelError("","Корзина пуста");
            }

            if(ModelState.IsValid) {
                allOrders.CreateOrder(order);
                //Console.WriteLine("Валидно");
                return RedirectToAction(nameof(Complete));
            }

            Console.WriteLine("Не валидно");                    
            return View(order);
        }

        [Route("Order/Complete")] 
        public IActionResult Complete() {
            ViewBag.Message = "Заказ успешно создан!";
            return View();
        }

    }
}