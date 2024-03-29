using Microsoft.AspNetCore.Mvc;
using TechStore.interfaces;
using TechStore.Models;
using TechStore.ViewModels;

namespace TechStore.Controllers {
    public class HomeController : Controller {
        private readonly IAllProducts _allProducts;

        public HomeController(IAllProducts iallp) {
            _allProducts = iallp;
        }

        [Route("/")]
        [Route("Home/List")]
        [Route("Home/List/{type}")]
        public ViewResult List(string type) {

            string _type = type;
            IEnumerable<Product> products = null;
            string currType = "";
            if(string.IsNullOrEmpty(type)) {
                products = _allProducts.Products.OrderBy(i => i.Id);
                currType = "Все товары";
            } else {
                if(string.Equals("Servers", type, StringComparison.OrdinalIgnoreCase)) {
                    products = _allProducts.Products.Where(i => i.Type.TypeName.Equals("Серверы")).OrderBy(i => i.Id);
                    currType = "Серверы";
                } else if (string.Equals("Computers", type, StringComparison.OrdinalIgnoreCase)) {
                    products = _allProducts.Products.Where(i => i.Type.TypeName.Equals("Компьютеры")).OrderBy(i => i.Id);
                    currType = "Компьютеры";
                } else if (string.Equals("NoteBooks", type, StringComparison.OrdinalIgnoreCase)) {
                    products = _allProducts.Products.Where(i => i.Type.TypeName.Equals("Ноутбуки")).OrderBy(i => i.Id);
                    currType = "Ноутбуки";
                }
            }
            
            var productObj = new HomeViewModel {
            allProducts = products,
            currType = currType
            };

            ViewBag.Title = "Tech-Store";

            return View(productObj);
        }
    }
} 