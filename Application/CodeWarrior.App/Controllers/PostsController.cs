using CodeWarrior.App.NewsFeed;
using CodeWarrior.App.ViewModels.Posts;
using CodeWarrior.DAL.DbContext;
using CodeWarrior.DAL.Interfaces;
using CodeWarrior.Model;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Web.Http;

namespace CodeWarrior.App.Controllers
{
    [Authorize]
    [RoutePrefix("api/Posts")]
    public class PostsController : BaseApiController
    {
        private readonly IPostRepository _postRepository;

        public PostsController(IApplicationDbContext applicationDbContext, IPostRepository postRepository)
            : base(applicationDbContext)
        {
            _postRepository = postRepository;
        }

        public IEnumerable<Post> Get()
        {
            var userRepository =
                (IUserRepository)
                    GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof (IUserRepository));
            return new NewsFeedBuilder(userRepository.FindById(User.Identity.GetUserId()), _postRepository).Build;
        }

        public Post Get(int id)
        {
            return _postRepository.FindById(id);
        }

        // POST api/post
        public IHttpActionResult Post(PostBindingModel post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var postModel = AutoMapper.Mapper.Map<PostBindingModel, Post>(post);

            postModel.PostedBy = User.Identity.GetUserId();

            _postRepository.Insert(postModel);

            return Ok(postModel);
        }

        // PUT api/post/5
        public IHttpActionResult Put(PostBindingModel post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var postModel = AutoMapper.Mapper.Map<PostBindingModel, Post>(post);
            var dbPost = _postRepository.FindById(post.Id);

            dbPost.Message = postModel.Message;

            _postRepository.Update(dbPost);

            return Ok();

        }

        // DELETE api/post/5
        public void Delete(string id)
        {
            _postRepository.Remove(id);
        }
    }
}
