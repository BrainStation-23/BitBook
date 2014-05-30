using MongoDB.AspNet.Identity;

namespace CodeWarrior.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}