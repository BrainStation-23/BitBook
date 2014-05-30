using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeWarrior.DAL.DbContext;
using CodeWarrior.DAL.Interfaces;
using CodeWarrior.Model;
using MongoDB.Driver.Builders;


namespace CodeWarrior.DAL.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IApplicationDbContext applicationDbContext) 
            : base(applicationDbContext)
        {

        }

        public MongoDB.Driver.MongoCursor<User> SearchByName(string name)
        {
            return Collection.Find(Query.Text(name, "english"));
        }
    }
}
