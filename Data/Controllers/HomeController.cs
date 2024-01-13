// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechStore.interfaces;
using TechStore.ViewModels;

namespace TechStore.Controllers {
    public class HomeController : Controller {
        private readonly IAllProducts _allProducts;
        private readonly IProductsType _allTypes;

        public HomeController(IAllProducts iallp, IProductsType ipt) {
            _allProducts = iallp;
            _allTypes = ipt;
        }

        public ViewResult List() {
            ViewBag.Title = "Tech-Store";
            HomeViewModel obj = new HomeViewModel();
            obj.allProducts = _allProducts.Products;
            obj.currType = "Серверы";

            return View(obj);
        }
    }
}