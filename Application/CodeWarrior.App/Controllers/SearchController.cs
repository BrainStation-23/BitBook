using CodeWarrior.App.ViewModels.Account;
using CodeWarrior.DAL.DbContext;
using CodeWarrior.DAL.Repositories;
using CodeWarrior.Model;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace CodeWarrior.App.Controllers
{
    [Authorize]
    [RoutePrefix("api/Search")]
    public class SearchController : BaseApiController
    {
        public SearchController(IApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {
        }

        public IEnumerable<object> Get([FromUri] ViewModels.SearchCriteria criteria)
        {
            switch (criteria.Type.ToLower())
            {
                case "user":
                    return SearchUser(criteria);
            }

            return null;
        }

        private IEnumerable<ApplicationUserViewModel> SearchUser(ViewModels.SearchCriteria criteria)
        {
            var id = User.Identity.GetUserId();
            var repository = new UserRepository(ApplicationDbContext);
            return
                repository.SearchByName(criteria.Key)
                    .Select(AutoMapper.Mapper.Map<ApplicationUser, ApplicationUserViewModel>)
                    .Where(model => model.Id!=id)
                    .ToList();
        }
    }
}