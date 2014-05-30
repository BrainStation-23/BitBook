using CodeWarrior.App.ViewModels.Account;
using CodeWarrior.App.ViewModels.Profile;
using CodeWarrior.DAL.DbContext;
using CodeWarrior.DAL.Interfaces;
using CodeWarrior.Model;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace CodeWarrior.App.Controllers
{
    [Authorize]
    public class ProfileController : BaseApiController
    {
        private readonly IUserRepository _userRepository;

        public ProfileController(IApplicationDbContext applicationDbContext, IUserRepository userRepository)
            : base(applicationDbContext)
        {
            _userRepository = userRepository;
        }

        // GET api/profile
        public ApplicationUserViewModel Get(string id = null)
        {
            var user = _userRepository.FindById(id ?? User.Identity.GetUserId());

            if (user != null)
            {
                user.FriendRequests = null;
                user.Friends = null;
            }
            return new ApplicationUserViewModel
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        public IHttpActionResult Put(ApplicationUserBindingModel applicationUser)
        {
            var dbUser = _userRepository.FindById(applicationUser.Id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dbUser.FirstName = applicationUser.FirstName;
            dbUser.LastName = applicationUser.LastName;
            _userRepository.Update(dbUser);

            return Ok(
                new ApplicationUserViewModel
                {
                    UserName = dbUser.UserName,
                    FirstName = dbUser.FirstName,
                    LastName = dbUser.LastName
                });
        }
    }
}