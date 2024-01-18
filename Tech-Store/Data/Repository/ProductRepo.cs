using Microsoft.EntityFrameworkCore;
using TechStore.interfaces;
using TechStore.Models;

namespace TechStore.Repository{
    public class ProductRepo : IAllProducts
    {
        private readonly AppDBContent appDBContent;
        public ProductRepo(AppDBContent appDBContent){
            this.appDBContent = appDBContent;
        }
        public IEnumerable<Product> Products => appDBContent.Product.Include(c => c.Type);

        public Product GetObjectProduct(int productId) => appDBContent.Product.FirstOrDefault(p => p.Id == productId);
    }
}