using CodeWarrior.DAL.DbContext;
using CodeWarrior.DAL.Interfaces;
using CodeWarrior.Model;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace CodeWarrior.App.Controllers
{
    //[Authorize]
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
            var user = _userRepository.FindById(id ?? User.Identity.GetUserId());

            if (user != null)
            {
                user.FriendRequests = null;
                user.Friends = null;
            }
            return user;
        }
    }
}
