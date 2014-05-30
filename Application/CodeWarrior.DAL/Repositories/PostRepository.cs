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
    }
}
