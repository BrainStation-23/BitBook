using CodeWarrior.App.Mappers;

namespace CodeWarrior.App
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            AutoMapperConfiguration.Configure();
        }
    }
}