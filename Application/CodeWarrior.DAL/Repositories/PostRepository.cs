using CodeWarrior.DAL.DbContext;
using CodeWarrior.DAL.Interfaces;
using CodeWarrior.Model;

namespace CodeWarrior.DAL.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(IApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {

        }

        public System.Collections.Generic.IEnumerable<Post> ByUser(string userId)
        {
            return Where(post => post.PostedBy == userId);
        }
    }
}
