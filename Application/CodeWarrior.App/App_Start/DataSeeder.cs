using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeWarrior.App.Controllers;
using CodeWarrior.DAL.DbContext;
using CodeWarrior.DAL.Interfaces;
using CodeWarrior.DAL.Repositories;
using CodeWarrior.Model;

namespace CodeWarrior.App.App_Start
{
    public class DataSeeder
    {
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;

        public DataSeeder()
        {
            var dbContext = new ApplicationDbContext();
            _userRepository = new UserRepository(dbContext);
            _postRepository = new PostRepository(dbContext);
        }

        private  IEnumerable<ApplicationUser> GetAllUser()
        {
            return _userRepository.FindAll();
        }

        public void SeedPosts(int count, int numberOfUser)
        {
            var users = GetAllUser();
            var userCount = 0;
            foreach (var user in users)
            {
                userCount++;
                if (userCount > numberOfUser) break;
                for (var i = 0; i < count; i++)
                {
                    _postRepository.Insert(
                        new Post
                        {
                            Message = Faker.TextFaker.Sentences(3),
                            Comments = new List<Comment>
                            {
                                new Comment
                                {
                                    Description = Faker.TextFaker.Sentence(),
                                    CommentedBy = user.Id
                                }
                            },
                            PostedBy = user.Id,
                            LikedBy = new List<string> { user.Id }
                        }
                        );
                }
            }
        }

        public void SeedUser(int count)
        {
            var users = GetAllUser();
            new AccountController().CreateFakeUser(count,users.Count());
        }
    }
}