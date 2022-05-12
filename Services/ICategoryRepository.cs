using StoreAPI.Models;
using System.Collections.Generic;

namespace StoreAPI.Services
{
    public interface ICategoryRepository : IRepositary<Category , int>
    {
        public List<Product> GetProductsbyCategoryID(int id);
        Category GetByName(string Name);

    }
}
