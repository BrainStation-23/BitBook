using MongoDB.Driver;

namespace CodeWarrior.DAL.DbContext
{
    public interface IApplicationDbContext
    {
        MongoDatabase Database { get; set; }
    }
}