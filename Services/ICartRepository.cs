using StoreAPI.Models;

namespace StoreAPI.Services
{
    public interface ICartRepository :IRepositary<Cart ,int>
    {
        public Cart GetByCustomerId(int id);
        public int InsertCartItem(CartItem cartItem);
        public CartItem GetCartItemById(int id);
        public int UpdateCartItem(int id, int Quantity);
        public int DeleteCartItem(int id);
    }
}
