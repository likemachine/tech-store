using Microsoft.AspNetCore.Mvc;
using TechStore.interfaces;
using TechStore.Models;
using TechStore.ViewModels;

namespace TechStore.Controllers {
    public class CartController : Controller {
        private IAllProducts _productRepo;
        private readonly Cart _cart;

        public CartController(IAllProducts productRepo, Cart cart) {
            _productRepo = productRepo;
            _cart = cart;
        }
        
        [Route("Cart/Index")]
        public ViewResult Index() {
            var items = _cart.getCartItems();
            _cart.ListCartItems = items;

            var obj = new CartViewModel {
                Cart = _cart
            };

            return View(obj);
        }

        [Route("Cart/addToCart")]
        public RedirectToActionResult addToCart (int id) {
            var item = _productRepo.Products.FirstOrDefault(i => i.Id == id);
            if(item != null) {
                _cart.AddToCart(item);
            }
            return RedirectToAction("Index");
        }

    }
}