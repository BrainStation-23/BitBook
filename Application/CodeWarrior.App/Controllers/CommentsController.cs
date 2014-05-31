using System;
using CodeWarrior.App.ViewModels.Account;
using CodeWarrior.App.ViewModels.Posts;
using CodeWarrior.DAL.DbContext;
using CodeWarrior.DAL.Interfaces;
using CodeWarrior.Model;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MongoDB.Bson;

namespace CodeWarrior.App.Controllers
{
    [Authorize]
    public class CommentsController : BaseApiController
    {
        private readonly IPostRepository _postRepository;

        private readonly IUserRepository _userRepository;

        public CommentsController(IApplicationDbContext applicationDbContext, IPostRepository postRepository,
            IUserRepository userRepository)
            : base(applicationDbContext)
        {
            _postRepository = postRepository;

            _userRepository = userRepository;
        }

        // POST api/comments
        public IHttpActionResult Post(CommentBindingModel comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            comment.CommentedBy = User.Identity.GetUserId();

            var post = _postRepository.FindById(comment.PostId);

            var commentTobeSaved = new Comment
            {
                CommentedBy = User.Identity.GetUserId(),
                Description = comment.Description,
                CommentedOn = DateTime.UtcNow,
                Id = ObjectId.GenerateNewId().ToString()
            };

            post.Comments.Add(commentTobeSaved);

            _postRepository.Update(post);

            var user = _userRepository.FindById(commentTobeSaved.CommentedBy);

            var commentViewModel = new CommentViewModel
            {
                CommentedBy = new ApplicationUserViewModel
                {
                    AvatarUrl = string.IsNullOrEmpty(user.AvatarUrl) ? "/Content/Images/noimage.png" : user.AvatarUrl,
                    FirstName = user.FirstName,
                    Id = user.Id,
                    LastName = user.LastName,
                    UserName = user.UserName
                },
                Id = commentTobeSaved.Id,
                Description = commentTobeSaved.Description,
                CommentedOn = commentTobeSaved.CommentedOn
            };

            return Ok(commentViewModel);
        }
    }
}
