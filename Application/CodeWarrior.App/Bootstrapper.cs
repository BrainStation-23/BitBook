using CodeWarrior.App.Mappers;
using CodeWarrior.DAL;
using CodeWarrior.DAL.DbContext;

namespace CodeWarrior.App
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            AutoMapperConfiguration.Configure();
            ConfigureDatabase.Configure();
        }
    }
}