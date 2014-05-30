using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeWarrior.App.ViewModels.Account;
using CodeWarrior.Model;

namespace CodeWarrior.App.ViewModels.Posts
{
    public class PostViewModel
    {
        public string Id { get; set; }

        public string Message { get; set; }

        public DateTime PostedOn { get; set; }

        public ApplicationUserViewModel PostedBy { get; set; }

        public int LikeCount { get; set; }

        public List<ApplicationUserViewModel> LikedBy { get; set; }
        
        public List<CommentViewModel> Comments { get; set; }

        public bool LikedByMe { get; set; }
    }
}