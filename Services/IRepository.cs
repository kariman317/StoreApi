using System;
using System.Collections.Generic;

namespace StoreAPI.Services
{
    public interface IRepositary<T ,T2>
        {
            Guid Id { get; set; }
            int Delete(T2 id);
            List<T> GetAll();
            T GetById(T2 id);

            int Insert(T obj);
            int Update(T2 id, T obj);
    }
}
