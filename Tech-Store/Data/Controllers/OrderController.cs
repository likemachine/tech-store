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
            
            //Console.WriteLine("Товаров в корзине = {0}", cart.ListCartItems.Count);
            if(cart.ListCartItems.Count == 0) {
                ModelState.AddModelError("","Корзина пуста");
                //Console.WriteLine("Ошибка не вывелась а товара нет");
            }

            if(ModelState.IsValid) {
                allOrders.CreateOrder(order);
                //Console.WriteLine("Валидно");
                return RedirectToAction(nameof(Complete));
            }

            Console.WriteLine("Не валидно");
            foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
                    
            return View(order);
        }

        [Route("Order/Complete")] 
        public IActionResult Complete() {
            ViewBag.Message = "Заказ успешно создан!";
            return View();
        }

    }
}