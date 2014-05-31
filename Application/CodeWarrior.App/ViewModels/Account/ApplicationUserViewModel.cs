
namespace CodeWarrior.App.ViewModels.Account
{
    public class ApplicationUserViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AvatarUrl { get; set; }

        public string UserName { get; set; }

        public bool IsMyFriend { get; set; }

        public bool? IsFriendRequestSent { get; set; }
    }
}
