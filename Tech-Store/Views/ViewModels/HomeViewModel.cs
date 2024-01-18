using TechStore.Models;

namespace TechStore.ViewModels{
    public class HomeViewModel{
        public IEnumerable<Product> allProducts { get; set; }

        public string currType { get; set; }
    }
}