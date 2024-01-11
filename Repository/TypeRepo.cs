using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TechStore.interfaces;
using TechStore.Models;

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