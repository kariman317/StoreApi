using StoreAPI.Models;

namespace StoreAPI.Services
{
    public interface IProductRepository :IRepositary<Product , int>
    {
        Product GetByName(string Name);

    }
}
