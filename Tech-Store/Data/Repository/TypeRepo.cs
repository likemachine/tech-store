
using TechStore.interfaces;

namespace TechStore.Repository{
    public class TypeRepo : IProductsType
    {
        private readonly AppDBContent appDBContent;
        public TypeRepo(AppDBContent appDBContent){
            this.appDBContent = appDBContent;
        }
        public IEnumerable<Models.Type> AllTypes => appDBContent.Type;
    }
}