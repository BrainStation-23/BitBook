using AutoMapper;
using CodeWarrior.App.ViewModels.Account;
using CodeWarrior.DAL.DbContext;
using CodeWarrior.DAL.Interfaces;
using CodeWarrior.Model;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CodeWarrior.App.Controllers
{
    [Authorize]
    [RoutePrefix("api/Friend")]
    public class FriendController : BaseApiController
    {
        private readonly IUserRepository _userRepository;

        public FriendController(IApplicationDbContext dbContext, IUserRepository userRepository)
            : base(dbContext)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<ApplicationUser> Get(string id = null)
        {
            var firends = new List<ApplicationUser>();
            var me = _userRepository.FindById(id ?? User.Identity.GetUserId());
            if (null == me.Friends) return firends;

            foreach (var friend in me.Friends.Select(friendId => _userRepository.FindById(friendId)))
            {
                friend.Friends = friend.FriendRequests = new List<string>();
                firends.Add(friend);
            }

            return firends;
        }

        [Route("Requests")]
        public IEnumerable<ApplicationUserViewModel> GetRequests()
        {
            var me = _userRepository.FindById(User.Identity.GetUserId());
            var pendingFriends = me.FriendRequests.Select(_userRepository.FindById).ToList();

            return pendingFriends.Select(Mapper.Map<ApplicationUser, ApplicationUserViewModel>).ToList();
        }

        public IHttpActionResult Post(string id)
        {
            var friend = _userRepository.FindById(id);
            if (null == friend) return BadRequest();

            var me = _userRepository.FindById(User.Identity.GetUserId());

            if (me.FriendRequests.Contains(friend.Id) || friend.FriendRequests.Contains(me.Id))
            {
                friend.FriendRequests.Remove(me.Id);
                friend.Friends.Add(me.Id);
                me.FriendRequests.Remove(friend.Id);
                me.Friends.Add(friend.Id);
                
                _userRepository.Update(me);
                _userRepository.Update(friend);
            }
            else
            {
                friend.FriendRequests.Add(me.Id);
                _userRepository.Update(friend);
            }

            return Ok();
        }

        public IHttpActionResult Delete(string id)
        {
            var friend = _userRepository.FindById(id);
            if (null == friend) return BadRequest();

            var me = _userRepository.FindById(User.Identity.GetUserId());
            friend.FriendRequests.Remove(me.Id);
            me.FriendRequests.Remove(friend.Id);
            me.Friends.Remove(friend.Id);
            friend.Friends.Remove(me.Id);

            _userRepository.Update(me);
            _userRepository.Update(friend);

            return Ok();
        }
    }
}
