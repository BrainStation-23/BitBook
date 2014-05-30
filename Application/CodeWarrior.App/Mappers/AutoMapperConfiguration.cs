using AutoMapper;

namespace CodeWarrior.App.Mappers
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x => x.AddProfile<BindingModelToDatabaseModel>());
            Mapper.Initialize(x => x.AddProfile<DatabaseModelToViewModel>());
        }
    }
}