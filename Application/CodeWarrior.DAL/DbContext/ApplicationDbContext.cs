using CodeWarrior.Model;
using CodeWarrior.SharedLibrary.Configurations;
using MongoDB.Driver;

namespace CodeWarrior.DAL.DbContext
{
    public class ApplicationDbContext
    {
        public MongoDatabase Database;

        public ApplicationDbContext()
        {
            var client = new MongoClient(MongoDbConfiguration.MongoDbClient);
            var server = client.GetServer();
            Database = server.GetDatabase(MongoDbConfiguration.DatabaseName);
        }

        public MongoCollection<Question> Questions
        {
            get { return Database.GetCollection<Question>("Questions"); }
        }
    }
}