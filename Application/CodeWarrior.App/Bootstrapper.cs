using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeWarrior.App.Mappers;

namespace CodeWarrior.App
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            //SetAutofacContainer();
            AutoMapperConfiguration.Configure();
        }
        //private static void SetAutofacContainer()
        //{
        //    //var builder = new ContainerBuilder();
        //    //builder.RegisterControllers(Assembly.GetExecutingAssembly());
        //    //builder.RegisterType<DefaultCommandBus>().As<ICommandBus>().InstancePerHttpRequest();
        //    //builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerHttpRequest();
        //    //builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerHttpRequest();
        //    //builder.RegisterAssemblyTypes(typeof(UserRepository).Assembly)
        //    //.Where(t => t.Name.EndsWith("Repository"))
        //    //.AsImplementedInterfaces().InstancePerHttpRequest();
        //    //var domainAssembly = Assembly.Load("Splash360.Domain");
        //    //builder.RegisterAssemblyTypes(domainAssembly)
        //    //.AsClosedTypesOf(typeof(ICommandHandler<>)).InstancePerHttpRequest();
        //    //builder.RegisterAssemblyTypes(domainAssembly)
        //    //.AsClosedTypesOf(typeof(IValidationHandler<>)).InstancePerHttpRequest();
        //    //builder.RegisterAssemblyTypes(domainAssembly)
        //    //    .Where(t => t.Name.EndsWith("Manager")).AsSelf();
        //    //builder.RegisterType<DefaultFormsAuthentication>().As<IFormsAuthentication>().InstancePerHttpRequest();
        //    //builder.RegisterFilterProvider();
        //    //IContainer container = builder.Build();
        //    //DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        //}
    }
}