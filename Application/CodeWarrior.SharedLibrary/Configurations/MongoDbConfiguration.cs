using System.Configuration;
using System.Web.Configuration;

namespace CodeWarrior.SharedLibrary.Configurations
{
    public class MongoDbConfiguration
    {
        public static string MongoDbClient
        {
            get { return ConfigurationManager.AppSettings["MongoDbClient"]; }
        }

        public static string DatabaseName
        {
            get { return ConfigurationManager.AppSettings["DatabaseName"]; }
        }

        public static string MongoConnection
        {
            get { return WebConfigurationManager.ConnectionStrings["MongoConnection"].ConnectionString; }
        }
    }
}