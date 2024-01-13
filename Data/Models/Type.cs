// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

namespace TechStore.Models {
    public class Type{
        public int Id { set; get; }
        public string TypeName { set; get; } = null!;
        //public string desc { get; set; }

        public List<Product> Products { set; get; } = null!;
    }
}