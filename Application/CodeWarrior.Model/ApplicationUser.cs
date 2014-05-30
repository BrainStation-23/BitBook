<<<<<<< HEAD
﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.AspNet.Identity;
using MongoDB.Bson.Serialization.Attributes;
=======
﻿using MongoDB.AspNet.Identity;
>>>>>>> db4b47fbd522d448aa9b3540d41f1acd10f353e0

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