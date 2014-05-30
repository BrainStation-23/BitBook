using System.Collections.Generic;
﻿using MongoDB.AspNet.Identity;

namespace CodeWarrior.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<string> Friends { get; set; }

        public List<string> FriendRequests { get; set; }
    }
}