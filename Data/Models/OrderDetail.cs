
namespace TechStore.Models{
    public class OrderDetail {
        public int Id { get; set; }

        public int OrderId { get; set; }
        
        public int ProductId { get; set; }
        
        public decimal Price { get; set; }
        
        public virtual Product product { get; set; }
        
        public virtual Order order { get; set; }
    }
}