using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechStore.Models;

namespace TechStore {
    public class DBObjects{
        public static void Initial(AppDBContent content) {

            if(!content.Type.Any())
                content.Type.AddRange(Types.Select(c => c.Value));

            if(!content.Product.Any()) {
                content.AddRange(
                    new Product {
                        Brand = "Apple", 
                        Model = "IMac A2115", 
                        Img = "/img/Imac.jpg", 
                        LongDesc = "2",
                        Price = 190000, 
                        Type = Types["Компьютеры"]
                    },
                    new Product {
                        Brand = "Acer", 
                        Model = "TC-1660", 
                        Img = "/img/Acer.jpg",
                        LongDesc = "2",
                        Price = 40000, 
                        Type = Types["Компьютеры"]
                    },
                    new Product {
                        Brand = "Asus", 
                        Model = "E1504FA-BQ719", 
                        Img = "/img/Asus.jpeg",
                        LongDesc = "2",
                        Price = 60000, 
                        Type = Types["Ноутбуки"]
                    },
                    new Product {
                        Brand = "Apple", 
                        Model = "MacBook Pro A2991", 
                        Img = "/img/MacBook.jpg",
                        LongDesc = "2",
                        Price = 380000, 
                        Type = Types["Ноутбуки"]
                    },
                    new Product {
                        Brand = "IRU", 
                        Model = "C2212P", 
                        Img = "/img/Iru.jpg",
                        LongDesc = "2",
                        Price = 370000, 
                        Type = Types["Серверы"]
                    },
                    new Product {
                        Brand = "DELL", 
                        Model = "T40", 
                        Img = "/img/DELL.png",
                        LongDesc = "2",
                        Price = 120000, 
                        Type = Types["Серверы"]
                    }
                );
            }

            content.SaveChanges();

        }

        public static Dictionary<string, Models.Type> type;
        public static Dictionary<string, Models.Type> Types {
            get {
                if(type == null) {
                    var list = new Models.Type[] {
                    new Models.Type { TypeName = "Серверы" },
                    new Models.Type { TypeName = "Компьютеры" },
                    new Models.Type { TypeName = "Ноутбуки" }
                    };

                    type = new Dictionary<string, Models.Type>();
                    foreach(Models.Type el in list)
                        type.Add(el.TypeName, el);
                }

                return type;
            }
        }
    }
}