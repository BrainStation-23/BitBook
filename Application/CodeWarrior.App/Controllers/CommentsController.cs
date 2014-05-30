using System;
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

        public CommentsController(IApplicationDbContext applicationDbContext, IPostRepository postRepository)
            : base(applicationDbContext)
        {
            _postRepository = postRepository;
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

            return Ok(commentTobeSaved);
        }
    }
}
