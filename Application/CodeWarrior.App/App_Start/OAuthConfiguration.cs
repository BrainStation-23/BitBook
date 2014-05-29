using System.Configuration;

namespace CodeWarrior.App
{
    public class OAuthConfiguration
    {
        public static string MicrosoftAuthClientId
        {
            get { return ConfigurationManager.AppSettings["MicrosoftAuthClientId"]; }
        }

        public static string MicrosoftAuthClientSecret
        {
            get { return ConfigurationManager.AppSettings["MicrosoftAuthClientSecret"]; }
        }

        public static string TwitterAuthConsumerKey
        {
            get { return ConfigurationManager.AppSettings["TwitterAuthConsumerKey"]; }
        }

        public static string TwitterAuthConsumerSecret
        {
            get { return ConfigurationManager.AppSettings["TwitterAuthConsumerSecret"]; }
        }

        public static string FacebookAuthAppId
        {
            get { return ConfigurationManager.AppSettings["FacebookAuthAppId"]; }
        }

        public static string FacebookAuthAppSecret
        {
            get { return ConfigurationManager.AppSettings["FacebookAuthAppSecret"]; }
        }

        public static string GoogleAuthClientId
        {
            get { return ConfigurationManager.AppSettings["GoogleAuthClientId"]; }
        }

        public static string GoogleAuthClientSecret
        {
            get { return ConfigurationManager.AppSettings["GoogleAuthClientSecret"]; }
        }
    }
}