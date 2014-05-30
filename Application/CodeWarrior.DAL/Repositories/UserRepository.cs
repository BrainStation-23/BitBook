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
    public class UserRepository : BaseRepository<ApplicationUser>, IUserRepository
    {
        public UserRepository(IApplicationDbContext applicationDbContext) 
            : base(applicationDbContext)
        {

        }

        public MongoDB.Driver.MongoCursor<ApplicationUser> SearchByName(string name)
        {
            return Collection.Find(Query.Text(name, "en"));
        }
    }
}
