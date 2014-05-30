using System.Collections.Generic;
﻿using MongoDB.AspNet.Identity;

namespace CodeWarrior.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string AvatarUrl { get; set; }

        private List<string> _friends;

        public List<string> Friends
        {
            get { return _friends ?? (_friends = new List<string>()); }
            set { _friends = value; }
        }

        private List<string> _friendRequests;

        public List<string> FriendRequests
        {
            get { return _friendRequests ?? (_friendRequests = new List<string>()); }
            set { _friendRequests = value; }
        }
    }
}