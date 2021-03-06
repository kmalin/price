﻿using System.Linq;

namespace Data.Generic
{
    public interface IGenericRepository<T> where T : IGenericEntity
    {
        IQueryable<T> GetAll();

        void Add(T entity);

        void Update(T entity);

        void Delete(long id);

        T GetById(long id);
    }
}
