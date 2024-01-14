// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechStore.interfaces;
using TechStore.Models;
using TechStore.ViewModels;

namespace TechStore.Controllers {
    public class HomeController : Controller {
        private readonly IAllProducts _allProducts;
        private readonly IProductsType _allTypes;

        public HomeController(IAllProducts iallp, IProductsType ipt) {
            _allProducts = iallp;
            _allTypes = ipt;
        }

        
        public ViewResult List(string type) {

            string _type = type;
            IEnumerable<Product> products = null;
            string currType = "";
            if(string.IsNullOrEmpty(type)) {
                products = _allProducts.Products.OrderBy(i => i.Id);
            } else {
                if(string.Equals("Серверы", type, StringComparison.OrdinalIgnoreCase)) {
                    products = _allProducts.Products.Where(i => i.Type.TypeName.Equals("Серверы")).OrderBy(i => i.Id);
                } else if (string.Equals("Компьютеры", type, StringComparison.OrdinalIgnoreCase)) {
                    products = _allProducts.Products.Where(i => i.Type.TypeName.Equals("Компьютеры")).OrderBy(i => i.Id);
                } else if (string.Equals("Ноутбуки", type, StringComparison.OrdinalIgnoreCase)) {
                    products = _allProducts.Products.Where(i => i.Type.TypeName.Equals("Ноутбуки")).OrderBy(i => i.Id);
                }

                currType = _type;
            }
            
            var productObj = new HomeViewModel {
            allProducts = products,
            currType = currType
            };

            ViewBag.Title = "Tech-Store";
            // HomeViewModel obj = new HomeViewModel();
            // obj.allProducts = _allProducts.Products;
            // obj.currType = "Серверы";

            return View(productObj);
        }
    }
} 