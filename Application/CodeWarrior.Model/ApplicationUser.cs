using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.AspNet.Identity;

namespace CodeWarrior.Model
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public List<string> Friends { get; set; }

        public List<string> FriendRequests { get; set; }
    }
}