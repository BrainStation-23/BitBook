using System.Web.Http;
using CodeWarrior.DAL.DbContext;

namespace CodeWarrior.App.Controllers
{
    public class BaseApiController : ApiController
    {
        protected IApplicationDbContext ApplicationDbContext { get; set; }

        public BaseApiController(IApplicationDbContext applicationDbContext)
        {
            ApplicationDbContext = applicationDbContext;
        }
    }
}