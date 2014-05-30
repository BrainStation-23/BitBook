using System;
using CodeWarrior.App.NewsFeed;
using CodeWarrior.App.ViewModels.Account;
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

        private IUserRepository _userRepository
        {
            get
            {
                return (IUserRepository)
                    GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IUserRepository));
            }
        }

        public PostsController(IApplicationDbContext applicationDbContext, IPostRepository postRepository)
            : base(applicationDbContext)
        {
            _postRepository = postRepository;
        }

        public IEnumerable<PostViewModel> Get()
        {
            var userRepository = _userRepository;
            return new NewsFeedBuilder(userRepository.FindById(User.Identity.GetUserId()), _postRepository, userRepository).BuildFeed();
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

            var postedBy = _userRepository.FindById(User.Identity.GetUserId());

            var model = new Post()
            {
                Message = post.Message,
                PostedBy = postedBy.Id,
                PostedOn = DateTime.UtcNow,
                LikedBy = new List<string>(),
                Comments = new List<Comment>()
            };
            model.PostedBy = User.Identity.GetUserId();
            _postRepository.Insert(model);

            var vModel = new PostViewModel
            {
                Id = model.Id,
                Message = post.Message,
                PostedBy = AutoMapper.Mapper.Map<ApplicationUser, ApplicationUserViewModel>(postedBy),
                LikeCount = model.LikeCount,
                LikedBy = new List<ApplicationUserViewModel>(),
                Comments = new List<CommentViewModel>()
            };

            return Ok(vModel);
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
        public IHttpActionResult Delete(string id)
        {
            _postRepository.Remove(id);

            return Ok();
        }
    }
}
