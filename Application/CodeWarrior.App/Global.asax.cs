using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CodeWarrior.App.App_Start;
using CodeWarrior.App.Controllers;

namespace CodeWarrior.App
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            UnityConfig.GetConfiguredContainer();

            Bootstrapper.Run();

            /*
            var seeder = new DataSeeder();
            seeder.SeedUser(1);
            seeder.SeedPosts(2,2);
            */
        }
    }
}