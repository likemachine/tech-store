
namespace TechStore.interfaces{
    public interface IProductsType{
        IEnumerable<Models.Type> AllTypes { get; }
    }
}