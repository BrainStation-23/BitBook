using System.Collections.Generic;
using System.Linq;
using CodeWarrior.DAL.Interfaces;
using CodeWarrior.Model;

namespace CodeWarrior.App.NewsFeed
{
    public class NewsFeedBuilder
    {
        private readonly ApplicationUser _user;
        private readonly IPostRepository _postRepository;

        public NewsFeedBuilder(ApplicationUser user, IPostRepository postRepository)
        {
            _user = user;
            _postRepository = postRepository;
        }

        private List<Post> _posts;

        public IEnumerable<Post> Posts
        {
            get
            {
                if (null == _posts)
                {
                    _posts = new List<Post>();
                    var feeders = _user.Friends;
                    feeders.Add(_user.Id);
                    foreach (var posts in feeders.Select(feeder => _postRepository.ByUser(feeder)))
                    {
                        _posts.AddRange(posts);
                    }
                }

                return _posts;
            }
        }
    }
}