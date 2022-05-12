using StoreAPI.Models;

namespace StoreAPI.Services
{
    public interface IUserRepository :IRepositary<User , string>
    {
        User GetByName(string Name);

    }
}
