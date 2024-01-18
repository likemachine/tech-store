using TechStore.interfaces;
using TechStore.Models;

namespace TechStore.mocks{
    public class MockProduct : IAllProducts {

        private readonly IProductsType _typeProduct = new MockType();

        public IEnumerable<Product> Products { 
            get {
                return new List<Product> {
                    new Product {
                        Brand = "Apple", 
                        Model = "IMac A2115", 
                        Img = "/img/Imac.jpg", 
                        Price = 190000, 
                        Type = _typeProduct.AllTypes.First()
                    },
                    new Product {
                        Brand = "Acer", 
                        Model = "TC-1660", 
                        Img = "/img/Acer.jpg", 
                        Price = 40000, 
                        Type = _typeProduct.AllTypes.First()
                    },
                    new Product {
                        Brand = "Asus", 
                        Model = "E1504FA-BQ719", 
                        Img = "/img/Asus.jpeg", 
                        Price = 60000, 
                        Type = _typeProduct.AllTypes.First()
                    },
                    new Product {
                        Brand = "Apple", 
                        Model = "MacBook Pro A2991", 
                        Img = "/img/MacBook.png", 
                        Price = 380000, 
                        Type = _typeProduct.AllTypes.First()
                    },
                    new Product {
                        Brand = "IRU", 
                        Model = "C2212P", 
                        Img = "/img/Iru.jpg", 
                        Price = 370000, 
                        Type = _typeProduct.AllTypes.First()
                    },
                    new Product {
                        Brand = "DELL", 
                        Model = "T40", 
                        Img = "/img/DELL.png", 
                        Price = 120000, 
                        Type = _typeProduct.AllTypes.First()
                    }
                };
            }
        }

        public Product GetObjectProduct(int productId)
        {
            throw new NotImplementedException();
        }
    }
}