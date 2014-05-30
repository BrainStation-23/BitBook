using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(CodeWarrior.App.Startup))]

namespace CodeWarrior.App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
          //  app.MapSignalR<>()
            ConfigureAuth(app);
            //var idProvider = new PrincipalUserIdProvider();
            //GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => idProvider);
        }
    }
}