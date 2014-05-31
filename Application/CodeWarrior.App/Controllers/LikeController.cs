using CodeWarrior.DAL.DbContext;
using CodeWarrior.DAL.Interfaces;
using CodeWarrior.Model;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace CodeWarrior.App.Controllers
{
    [Authorize]
    [RoutePrefix("api/like")]
    public class LikeController : BaseApiController
    {
        private readonly IPostRepository _postRepository;

        public LikeController(IApplicationDbContext applicationDbContext, IPostRepository postRepository)
            : base(applicationDbContext)
        {
            _postRepository = postRepository;
        }

        public IHttpActionResult Post(string id)
        {
            var post = GetPost(id);
            post.LikedBy.Add(User.Identity.GetUserId());
            _postRepository.Update(post);

            return Ok();
        }

        private Post GetPost(string id)
        {
            return _postRepository.FindById(id);
        }

        public IHttpActionResult Delete(string id)
        {
            var post = GetPost(id);
            post.LikedBy.Remove(User.Identity.GetUserId());
            _postRepository.Update(post);

            return Ok();
        }
    }
}
