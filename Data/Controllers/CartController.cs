using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Tech_Store.Migrations;
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

        public ViewResult Index() {
            var items = _cart.getShopItems();
            _cart.ListCartItems = items;

            var obj = new CartViewModel {
                Cart = _cart
            };

            return View(obj);
        }

        public RedirectToActionResult addToCart (int id) {
            var item = _productRepo.Products.FirstOrDefault(i => i.Id == id);
            if(item != null) {
                _cart.AddToCart(item);
            }
            return RedirectToAction("Index");
        }

    }
}