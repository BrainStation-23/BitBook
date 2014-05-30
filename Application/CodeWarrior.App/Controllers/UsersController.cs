using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CodeWarrior.App.ViewModels.Questions;
using CodeWarrior.DAL.DbContext;
using CodeWarrior.DAL.Interfaces;
using CodeWarrior.Model;

namespace CodeWarrior.App.Controllers
{
    //[Authorize]
    [RoutePrefix("api/Users")]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IApplicationDbContext applicationDbContext, IUserRepository userRepository)
            : base(applicationDbContext)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<ApplicationUser> SerachByName(string name)
        {
            return _userRepository.SearchByName(name);
        }

        public IEnumerable<ApplicationUser> Get()
        {
            return _userRepository.FindAll();
        }

        public ApplicationUser Get(string id)
        {
            return _userRepository.FindById(id);
        }
    }
}
