
namespace TechStore.Models{
    public class CartItem{
        public int Id { get; set; }
        public Product Product { get; set; }
        public decimal Price { get; set; }

        public string CartId { get; set; }
    }
}