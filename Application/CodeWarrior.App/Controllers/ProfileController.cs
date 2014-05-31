﻿using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using CodeWarrior.App.ViewModels.Account;
using CodeWarrior.App.ViewModels.Profile;
using CodeWarrior.DAL.DbContext;
using CodeWarrior.DAL.Interfaces;
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
            var myId = User.Identity.GetUserId();
            var me = _userRepository.FindById(myId);
            var user = null == id ? me : _userRepository.FindById(id);

            return new ApplicationUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsMyFriend = myId != id && me.Friends.Contains(id),
                IsFriendRequestSent = myId != id && !me.Friends.Contains(id) && user.FriendRequests.Contains(id)
            };
        }

        public IHttpActionResult Put(ApplicationUserBindingModel applicationUser)
        {
            var dbUser = _userRepository.FindById(User.Identity.GetUserId());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dbUser.FirstName = applicationUser.FirstName;
            dbUser.LastName = applicationUser.LastName;
            _userRepository.Update(dbUser);

            return Ok();
        }

        // POST api/Profile/Upload
        public IHttpActionResult Post()
        {
            if (HttpContext.Current.Request.Files.Count == 0)
            {
                return BadRequest();
            }

            var file = HttpContext.Current.Request.Files[0];
            var url = BuildAvatarUrl(file.FileName);
            file.SaveAs(url);
            var user = _userRepository.FindById(User.Identity.GetUserId());
            user.AvatarUrl = url;
            _userRepository.Update(user);

            return Ok();
        }

        private string BuildAvatarUrl(string name)
        {
            return Path.Combine(User.Identity.GetUserId(), "_avatar", (Path.GetExtension(name) ?? ".png"));
        }

        public class UploadBindingModel
        {
            public HttpPostedFile Avatar { get; set; }
        }
    }
}