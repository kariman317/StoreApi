using StoreAPI.Models;
using StoreAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreAPI.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly Store store;

        public CartRepository(Store store)
        {
            this.store = store;
        }
        public CartRepository()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set ; }
        public List<Cart> GetAll()
        {
            return store.Cart.ToList();
        }

        public Cart GetByCustomerId(int id)
        {
            return store.Cart.FirstOrDefault(x => x.UserId == id.ToString());

        }

        public Cart GetById(int id)
        {
            return store.Cart.FirstOrDefault(x=>x.Id ==id);
        }

        public CartItem GetCartItemById(int id)
        {
            return store.CartItem.FirstOrDefault(c => c.Id == id);
        }

        public int Insert(Cart cart)
        {
            store.Cart.Add(cart);
            return store.SaveChanges();
        }

        public int InsertCartItem(CartItem cartItem)
        {
            store.CartItem.Add(cartItem);
            return store.SaveChanges();
        }

        public int Update(int id, Cart dept)
        {
            Cart oldCart = GetById(id);
            if (oldCart != null)
            {
                return store.SaveChanges();
            }
            return 0;
        }

        public int UpdateCartItem(int id, int Quantity)
        {
            CartItem oldCartItem = GetCartItemById(id);
            if (oldCartItem != null)
            {
                oldCartItem.Quantity = Quantity;
                return store.SaveChanges();
            }
            return 0;
        }
        public int Delete(int id)
        {
            Cart oldCart = GetById(id);
            store.Cart.Remove(oldCart);
            return store.SaveChanges();
        }

        public int DeleteCartItem(int id)
        {
            store.CartItem.Remove(GetCartItemById(id));
            return store.SaveChanges();
        }

       
    }
}
