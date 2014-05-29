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
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(IApplicationDbContext applicationDbContext) 
            : base(applicationDbContext)
        {

        }
    }
}