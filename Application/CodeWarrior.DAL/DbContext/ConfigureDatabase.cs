using System;
using System.Linq.Expressions;
using CodeWarrior.Model;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace CodeWarrior.DAL.DbContext
{
    public static class ConfigureDatabase
    {
        internal static string Name<T>(Expression<Func<T, string>> expression)
        {
            var member = expression.Body as MemberExpression;
            if (null == member) throw new Exception("Only member expression type allowed.");

            return member.Member.Name;
        }

        public static void Configure()
        {
            IApplicationDbContext dataContext = new ApplicationDbContext();
            var dataBae = dataContext.Database;

            var users = dataBae.GetCollection<ApplicationUser>(typeof (ApplicationUser).Name);
            var indexBuilder = new IndexKeysBuilder();
            var indexByFirstAndLastName = indexBuilder.Text(Name<ApplicationUser>(au => au.FirstName),
                Name<ApplicationUser>(au => au.LastName));
           // users.CreateIndex(indexByFirstAndLastName, IndexOptions.Null);
        }
    }
}
