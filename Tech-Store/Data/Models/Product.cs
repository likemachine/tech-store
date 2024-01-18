
namespace TechStore.Models {
    public class Product{
        public int Id { set; get; }
        public string Brand { set; get; }
        public string Model { set; get; }
        public string LongDesc { set; get; }
        public string Img { set; get; }
        public decimal Price { set; get; }
        public int TypeId { set; get; }

        public virtual Type Type { set; get; }
    }
}