using CodeWarrior.App.ViewModels.Account;
using CodeWarrior.DAL.DbContext;
using CodeWarrior.DAL.Interfaces;
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
            var users = repository.SearchByName(criteria.Key)
                .Where(model => model.Id != id)
                .ToList();
            var me = repository.FindById(id);

            return ApplicationUserToViewModel(me, users);
        }

        public static IEnumerable<ApplicationUserViewModel> ApplicationUserToViewModel(ApplicationUser me, IEnumerable<ApplicationUser> users)
        {
            var vUsers = new List<ApplicationUserViewModel>();
            foreach (var user in users)
            {
                var vUser = AutoMapper.Mapper.Map<ApplicationUser, ApplicationUserViewModel>(user);
                var isMyFriend = me.Id != user.Id && me.Friends.Contains(user.Id);
                vUser.IsMyFriend = isMyFriend;
                vUser.IsFriendRequestSent = !isMyFriend && user.FriendRequests.Contains(me.Id);
                vUser.AvatarUrl = user.AvatarUrl ?? "/Content/Images/noimage.png";
                vUsers.Add(vUser);
            }

            return vUsers;
        }
    }
}