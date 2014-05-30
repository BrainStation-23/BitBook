using System.Collections;
using System.Collections.Generic;
using CodeWarrior.Model;

namespace CodeWarrior.DAL.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        IEnumerable<Post> ByUser(string userId);
    }
}
