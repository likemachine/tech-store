// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.IO;
// using System.Threading.Tasks;
using TechStore.Models;

namespace TechStore.ViewModels{
    public class HomeViewModel{
        public IEnumerable<Product> allProducts { get; set; }

        public string currType { get; set; }
    }
}