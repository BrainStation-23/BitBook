using System.Collections.Generic;
using System.Linq;
using CodeWarrior.App.ViewModels.Account;
using CodeWarrior.App.ViewModels.Posts;
using CodeWarrior.DAL.Interfaces;
using CodeWarrior.Model;

namespace CodeWarrior.App.NewsFeed
{
    public class NewsFeedBuilder
    {
        private readonly ApplicationUser _user;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public NewsFeedBuilder(ApplicationUser user, IPostRepository postRepository, IUserRepository userRepository)
        {
            _user = user;
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        private List<PostViewModel> _postViews;

        public IEnumerable<PostViewModel> BuildFeed()
        {
            if (null == _postViews)
            {
                _postViews = new List<PostViewModel>();
                var feeders = _user.Friends;
                feeders.Add(_user.Id);

                var posts = new List<Post>();
                foreach (var feeder in feeders)
                {
                    posts.AddRange(_postRepository.ByUser(feeder));
                }

                var users = ParseUniqueUser(posts);

                foreach (var post in posts)
                {
                    var view = new PostViewModel
                    {
                        Id = post.Id,
                        Message = post.Message,
                        PostedBy = users[post.PostedBy],
                        PostedOn = post.PostedOn,
                        LikedBy = post.LikedBy.Select(s => users[s]).ToList()
                    };
                    view.LikeCount = view.LikedBy.Count;

                    var vComments = post.Comments.Select(comment => new CommentViewModel
                    {
                        CommentedOn = comment.CommentedOn.ToLocalTime(),
                        Description = comment.Description,
                        Id = comment.Id,
                        CommentedBy = users[comment.CommentedBy]
                    }).ToList();
                    view.Comments = vComments;

                    view.LikedByMe = post.LikedBy.Any(u => u.Equals(_user.Id));

                    _postViews.Add(view);
                }

                _postViews = _postViews.OrderByDescending(model => model.PostedOn).Take(30).ToList();
            }

            return _postViews;
        }

        private Dictionary<string, ApplicationUserViewModel> ParseUniqueUser(IEnumerable<Post> posts)
        {
            var userIds = new List<string>();
            foreach (var post in posts)
            {
                userIds.Add(post.PostedBy);
                if (post.LikedBy.Count > 0) userIds.AddRange(post.LikedBy);
                if (null != post.Comments && post.Comments.Count > 0)
                {
                    userIds.AddRange(post.Comments.Select(comment => comment.CommentedBy));
                }
            }

            var users = new Dictionary<string, ApplicationUserViewModel>();
            foreach (var userId in userIds)
            {
                if (users.ContainsKey(userId)) continue;

                var user = _userRepository.FindById(userId);
                users[userId] = AutoMapper.Mapper.Map<ApplicationUser, ApplicationUserViewModel>(user);
            }

            return users;
        }
    }
}