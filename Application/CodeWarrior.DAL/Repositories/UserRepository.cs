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
            Collection = ApplicationDbContext.Database.GetCollection<ApplicationUser>("AspNetUsers");
        }

        public MongoDB.Driver.MongoCursor<ApplicationUser> SearchByName(string name)
        {
            return Collection.Find(Query.Text(name, "en"));
        }
    }
}