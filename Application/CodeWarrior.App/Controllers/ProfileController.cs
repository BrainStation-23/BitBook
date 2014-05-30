using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CodeWarrior.DAL.DbContext;
using CodeWarrior.DAL.Interfaces;
using CodeWarrior.Model;
using MongoDB.AspNet.Identity;

namespace CodeWarrior.App.Controllers
{
    public class ProfileController : BaseApiController
    {
        private readonly IUserRepository _userRepository;

        public ProfileController(IApplicationDbContext applicationDbContext, IUserRepository userRepository)
            : base(applicationDbContext)
        {
            _userRepository = userRepository;
        }

        // GET api/profile
        public ApplicationUser Get(string id = null)
        {
            return null;
        }
    }
}
