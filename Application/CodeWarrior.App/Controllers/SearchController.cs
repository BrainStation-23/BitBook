using CodeWarrior.App.ViewModels;
using CodeWarrior.DAL.DbContext;
using CodeWarrior.DAL.Repositories;
using CodeWarrior.Model;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CodeWarrior.App.Controllers
{
    //[Authorize]
    [RoutePrefix("api/Search")]
    public class SearchController : BaseApiController
    {
        public SearchController(IApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {
        }

        public IEnumerable<object> Get([FromUri] SearchCriteria criteria)
        {
            switch (criteria.Type.ToLower())
            {
                case "user":
                    return SearchUser(criteria);
            }

            return null;
        }

        private IEnumerable<ApplicationUser> SearchUser(SearchCriteria criteria)
        {
            var repository = new UserRepository(ApplicationDbContext);
            var users = repository.SearchByName(criteria.Key);
            return users.ToList();
        }
    }
}
