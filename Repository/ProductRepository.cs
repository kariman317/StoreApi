using StoreAPI.Models;
using StoreAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly Store store;

        public ProductRepository(Store store)
        {
            this.store = store;
        }
        public ProductRepository()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id 
        { get ; set ; }
        public List<Product> GetAll()
        {
            return store.Product.ToList();
        }

        public Product GetById(int id)
        {
            return store.Product.FirstOrDefault(p=>p.Id ==id);
        }

        public Product GetByName(string Name)
        {
            return store.Product.FirstOrDefault(p => p.Name == Name);
        }

        public int Insert(Product product)
        {
            store.Product.Add(product);
            return store.SaveChanges();
        }

        public int Update(int id, Product product)
        {
            Product oldPrd = GetById(id);
            if (oldPrd != null)
            {
                oldPrd.Name = product.Name;
                oldPrd.Price = product.Price;
                oldPrd.Image = product.Image;
                oldPrd.Quantity = product.Quantity;
                return store.SaveChanges();
            }
            return 0;
        }
        public int Delete(int id)
        {
            Product product = GetById(id);
            store.Product.Remove(product);
            return store.SaveChanges();
        }
    }
}
