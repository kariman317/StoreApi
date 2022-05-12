using StoreAPI.Models;
using StoreAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly Store store;

        public UserRepository(Store store)
        {
            this.store = store;
        }
        public UserRepository()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id
        { get; set; }

        

        public List<User> GetAll()
        {
            return store.Users.ToList();
        }

        public User GetById(string id)
        {
            return store.UserApp.FirstOrDefault(u => u.Id == id);
        }

        public int Insert(User user)
        {
            store.UserApp.Add(user);
            return store.SaveChanges();
        }

        public int Update(string id, User user)
        {
            User oldUser = GetById(id);
            if (oldUser != null)
            {

                return store.SaveChanges();
            }
            return 0;
        }
       
        public int Delete(string id)
        {
            User oldUser = GetById(id);
            store.UserApp.Remove(oldUser);
            return store.SaveChanges();
        }

        public User GetByName(string Name)
        {
            return store.UserApp.FirstOrDefault(u => u.UserName == Name);

        }
    }
}
