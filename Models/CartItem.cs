using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechStore.Models{
    public class CartItem{
        public int ItemId { get; set; }
        public Product Product { get; set; }
        public int Price { get; set; }

        public string CartId { get; set; }
    }
}