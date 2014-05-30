using System;
using CodeWarrior.App.ViewModels.Account;

namespace CodeWarrior.App.ViewModels.Posts
{
    public class CommentViewModel
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public ApplicationUserViewModel CommentedBy { get; set; }

        public DateTime CommentedOn { get; set; }
    }
}
