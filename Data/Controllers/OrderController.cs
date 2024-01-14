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

        [HttpPost]
        [Route("Order/Checkout")]
        public IActionResult Checkout(Order order) {
            
            cart.ListCartItems = cart.getCartItems();

            if(cart.ListCartItems.Count == 0) {
                ModelState.AddModelError("","Корзина пуста"); //Ошибка не выводится
            }

            if(ModelState.IsValid) {
                allOrders.CreateOrder(order);
                return RedirectToAction("Complete");
            }

            return View(order);
        }

        //Функция комплит не вызывается, пробелма может быть в роутинге 
        [Route("Order/Complete")] 
        public IActionResult Complete() {
            ViewBag.Message = "Заказ успешно создан!";
            return View();
        }

    }
}