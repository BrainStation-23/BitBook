using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeWarrior.Model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CodeWarrior.DAL.Interfaces
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        IEnumerable<ApplicationUser> SearchByName(string name);
    }
}
