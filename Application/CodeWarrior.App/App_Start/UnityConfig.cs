using System;
using CodeWarrior.App.Controllers;
using CodeWarrior.DAL.DbContext;
using CodeWarrior.DAL.Interfaces;
using CodeWarrior.DAL.Repositories;
using Microsoft.Practices.Unity;

namespace CodeWarrior.App
{
    public class UnityConfig
    {
        private static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return Container.Value;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterTypes(
                AllClasses.FromLoadedAssemblies(),
                WithMappings.FromMatchingInterface,
                WithName.Default
                );

            container.RegisterType<AccountController>(new InjectionConstructor());

            /*
            container.RegisterType<IApplicationDbContext, ApplicationDbContext>();
            container.RegisterType<IQuestionRepository, QuestionRepository>();
            container.RegisterType<IPostRepository, PostRepository>();
             */
        }
    }
}