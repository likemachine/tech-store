using TechStore.interfaces;
using TechStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechStore.mocks {
    public class MockType : IProductsType
    {
        public IEnumerable<Models.Type> AllTypes{
            get {
                return new List<Models.Type> {
                    new Models.Type { TypeName = "Серверы" },
                    new Models.Type { TypeName = "Компьютеры" },
                    new Models.Type { TypeName = "Ноутбуки" }
                };
            }
        }
    }
}
