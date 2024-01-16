using TechStore.Models;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

namespace TechStore.interfaces{
    public interface IAllProducts{
        IEnumerable<Product> Products { get; }
        Product GetObjectProduct(int productId);
    }
}