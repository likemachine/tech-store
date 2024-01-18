
namespace TechStore.Models {
    public class Type{
        public int Id { set; get; }
        public string TypeName { set; get; }
        //public string desc { get; set; }

        public List<Product> Products { set; get; }
    }
}