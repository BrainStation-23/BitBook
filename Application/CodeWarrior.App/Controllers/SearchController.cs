using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CodeWarrior.App.ViewModels;
using CodeWarrior.App.ViewModels.Questions;
using CodeWarrior.DAL.DbContext;
using CodeWarrior.DAL.Interfaces;
using CodeWarrior.DAL.Repositories;
using CodeWarrior.Model;

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

        public IEnumerable<object> Get(SearchCriteria criteria)
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
            return new UserRepository(ApplicationDbContext).SearchByName(criteria.Key);
        }
    }
}
