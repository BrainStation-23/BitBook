using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CodeWarrior.DAL.DbContext;
using CodeWarrior.DAL.Interfaces;
using CodeWarrior.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;


namespace CodeWarrior.DAL.Repositories
{
    public class UserRepository : BaseRepository<ApplicationUser>, IUserRepository
    {
        public UserRepository(IApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {
            Collection = ApplicationDbContext.Database.GetCollection<ApplicationUser>("AspNetUsers");
        }

        public IEnumerable<ApplicationUser> SearchByName(string name)
        {
            return Collection.AsQueryable()
                .Where( user => user.UserName.Contains(name)
                            || user.FirstName.Contains(name)
                            || user.LastName.Contains(name));
        }
    }
}
