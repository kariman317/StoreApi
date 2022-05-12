using Microsoft.EntityFrameworkCore;
using StoreAPI.Models;
using StoreAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreAPI.Repository
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly Store store;

        public CategoryRepository(Store store)
        {
            this.store = store;
        }
        public CategoryRepository()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id 
        { get ; set; }
        public List<Category> GetAll()
        {
            return store.Category.ToList();
        }

        public Category GetById(int id)
        {
            return store.Category.Include(c=>c.Products).FirstOrDefault(c=>c.Id ==id); 
        }

        public Category GetByName(string Name)
        {
            return store.Category.Include(c => c.Products).FirstOrDefault(c => c.Name == Name);

        }

        public List<Product> GetProductsbyCategoryID(int id)
        {
            return store.Product.Where(x => x.CategoryId == id).ToList();
        }

        public int Insert(Category category)
        {
            store.Category.Add(category);
            return store.SaveChanges();   
        }
        public int Update(int id, Category category)
        {
            Category oldCat = GetById(id);
            if (oldCat != null)
            {
                oldCat.Name = category.Name;
                return store.SaveChanges();
            }
            return 0;
        }
        public int Delete(int id)
        {
            Category category = GetById(id);
            store.Category.Remove(category);
            return store.SaveChanges();
        }        
    }
}
