using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Driver;

namespace CodeWarrior.DAL.Interfaces
{
    public interface IRepository<T>
        where T : class
    {
        T FindById<TKey>(TKey id);
        MongoCursor<T> FindAll(IMongoQuery query = null);
        IEnumerable<T> Where(Expression<Func<T, bool>> where = null);
        void Insert(T entity);
        void Update(T entity);
        void Remove<TKey>(TKey id);
        void RemoveAll();
    }
}