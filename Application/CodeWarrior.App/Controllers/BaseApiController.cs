using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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