using System;
using System.Collections.Generic;
using System.Linq;
using CodeWarrior.DAL.DbContext;
using CodeWarrior.DAL.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace CodeWarrior.DAL.Repositories
{

    public class BaseRepository<T> : IRepository<T>
        where T : class

    {
        protected readonly IApplicationDbContext ApplicationDbContext;
        protected MongoCollection<T> Collection;

        public BaseRepository(IApplicationDbContext applicationDbContext)
        {
            ApplicationDbContext = applicationDbContext;
            Collection = ApplicationDbContext.Database.GetCollection<T>(typeof (T).Name);
        }

        public T FindById<TKey>(TKey id)
        {
            return Collection.FindOneById(new ObjectId(Convert.ToString(id)));
        }

        public MongoCursor<T> FindAll(IMongoQuery query = null)
        {
            return null == query ? Collection.FindAll() : Collection.Find(query);
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> predicate = null)
        {
            var entities = Collection.AsQueryable();
            if (null != predicate) entities = entities.Where(predicate);

            return entities;
        }

        public void Insert(T entity)
        {
            Collection.Insert(entity);
        }

        public void Update(T entity)
        {
            Collection.Save(entity);
        }

        public void Remove<TKey>(TKey id)
        {
            Collection.Remove(Query.EQ("_id", new ObjectId(Convert.ToString(id))));
        }

        public void RemoveAll()
        {
            Collection.RemoveAll();
        }
    }
}