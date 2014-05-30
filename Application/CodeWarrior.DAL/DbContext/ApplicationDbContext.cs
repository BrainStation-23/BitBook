using CodeWarrior.Model;
using CodeWarrior.SharedLibrary.Configurations;
using MongoDB.Driver;

namespace CodeWarrior.DAL.DbContext
{
    public class ApplicationDbContext : IApplicationDbContext
    {
        public MongoDatabase Database { get; set; }

        public ApplicationDbContext()
        {
            var client = new MongoClient(MongoDbConfiguration.MongoDbClient);
            var server = client.GetServer();
            Database = server.GetDatabase(MongoDbConfiguration.DatabaseName);
        }

        //public MongoCollection<Question> Questions
        //{
        //    get { return Database.GetCollection<Question>(typeof (Question).Name); }
        //}

    }
}