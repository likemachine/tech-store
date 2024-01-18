using TechStore.Models;

namespace TechStore.interfaces{
    public interface IAllProducts{
        IEnumerable<Product> Products { get; }
        Product GetObjectProduct(int productId);
    }
}