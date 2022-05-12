using StoreAPI.Models;
using StoreAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreAPI.Repository
{
    public class OrderRepository :IOrderRepository
    {
        private readonly Store store;
        public OrderRepository(Store store)
        {
            this.store=store;
        }
        public OrderRepository()
        {
            Id=Guid.NewGuid();
        }
        public Guid Id 
        { get ;set; }
        public List<Order> GetAll()
        {
            return store.Order.ToList();
        }

        public Order GetById(int id)
        {
            return store.Order.FirstOrDefault(o=>o.Id==id);
        }

        public int Insert(Order order)
        {
            store.Order.Add(order);
            return store.SaveChanges();
        }

        public int Update(int id, Order order)
        {
            Order oldOrder = GetById(id);
            if (oldOrder != null)
            {
                oldOrder.TotalPrice = order.TotalPrice;
                return store.SaveChanges();
            }
            return 0;
        }
        public int Delete(int id)
        {
            Order oldOrder = GetById(id);
            store.Order.Remove(oldOrder);
            return store.SaveChanges();
        }

       
    }
}
