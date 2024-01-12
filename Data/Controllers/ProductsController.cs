using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechStore.interfaces;
using TechStore.ViewModels;

namespace TechStore.Controllers {
    public class ProductsController : Controller {
        private readonly IAllProducts _allProducts;
        private readonly IProductsType _allTypes;

        public ProductsController(IAllProducts iallp, IProductsType ipt) {
            _allProducts = iallp;
            _allTypes = ipt;
        }

        public ViewResult List() {
            ViewBag.Title = "Tech-Store";
            ProductsListViewModel obj = new ProductsListViewModel();
            obj.allProducts = _allProducts.Products;
            obj.currType = "Серверы";

            return View(obj);
        }
    }
}