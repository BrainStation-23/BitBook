using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.AspNet.Identity;
using MongoDB.Bson.Serialization.Attributes;

namespace CodeWarrior.Model
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [BsonRequired]
        [EmailAddress]
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<string> Friends { get; set; }

        public List<string> FriendRequests { get; set; }
    }
}