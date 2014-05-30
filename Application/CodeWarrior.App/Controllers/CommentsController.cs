using CodeWarrior.App.ViewModels.Posts;
using CodeWarrior.DAL.DbContext;
using CodeWarrior.DAL.Interfaces;
using CodeWarrior.Model;
using System.Web.Http;
using Microsoft.AspNet.Identity;

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

            var commentModel = AutoMapper.Mapper.Map<CommentBindingModel, Comment>(comment);

            commentModel.CommentedBy = User.Identity.GetUserId();

            var post = _postRepository.FindById(comment.PostId);

            post.Comments.Add(commentModel);

            _postRepository.Update(post);

            return Ok(commentModel);
        }
    }
}
