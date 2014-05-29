using System;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Driver;

namespace CodeWarrior.DAL.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        TEntity FindById(string id);
        MongoCursor<TEntity> FindAll();
        IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(int id);
    }
}