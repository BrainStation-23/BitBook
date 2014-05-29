using System;
using System.Linq;
using System.Linq.Expressions;
using CodeWarrior.DAL.DbContext;
using CodeWarrior.DAL.Interfaces;
using CodeWarrior.Model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CodeWarrior.DAL.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public QuestionRepository()
        {
            _dbContext = new ApplicationDbContext();
        }

        public Question FindById(string id)
        {
            return _dbContext.Questions.FindOneById(new ObjectId(id));
        }

        public MongoCursor<Question> FindAll()
        {
            return _dbContext.Questions.FindAll();
        }

        public IQueryable<Question> FindAll(Expression<Func<Question, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Insert(Question entity)
        {
            _dbContext.Questions.Insert(entity);
        }

        public void Update(Question entity)
        {
            var question = FindById(entity.Id);
            _dbContext.Questions.Save(question);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}