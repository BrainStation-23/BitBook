using CodeWarrior.App.Controllers;
using CodeWarrior.DAL.DbContext;
using CodeWarrior.DAL.Interfaces;
using CodeWarrior.DAL.Repositories;
using CodeWarrior.Model;
using Microsoft.AspNet.Identity;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;

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
                                    CommentedBy = user.Id,
                                    CommentedOn = DateTime.UtcNow,
                                    Id = ObjectId.GenerateNewId().ToString()
                                }
                            },
                            PostedBy = user.Id,
                            LikedBy = new List<string> { user.Id },
                            PostedOn = DateTime.UtcNow
                        }
                        );
                }
            }
        }

        public void SeedUser(AccountController accountController, int count)
        {
            var uCount = GetAllUser().Count() + 1;

            var fakeUsers = Enumerable.Range(0, count).Select(i => new ApplicationUser
            {
                Id = ObjectId.GenerateNewId().ToString(),
                FirstName = Faker.NameFaker.FirstName(),
                LastName = Faker.NameFaker.LastName(),
                UserName = (uCount + i) + Faker.InternetFaker.Email()
            }).ToList();

            var random = new Random();

            var userIds = fakeUsers.Select(fakeUser => fakeUser.Id).ToList();

            foreach (var fakeUser in fakeUsers)
            {
                var index = random.Next(0, count);
                var last = index + 10;
                if (last > count) last = count;
                if(last == index) continue;
                var randomIds = userIds.GetRange(index, last-index);
                fakeUser.Friends.AddRange(randomIds);
                foreach (var result in fakeUsers.Where(user => randomIds.Contains(user.Id)))
                {
                    result.Friends.Add(fakeUser.Id);
                }
            }

            foreach (var fakeUser in fakeUsers)
            {
                fakeUser.Friends = fakeUser.Friends.Distinct().ToList();
            }


            foreach (var fakeUser in fakeUsers)
            {
                accountController.UserManager.Create(fakeUser, "12345678");
            }
        }
    }
}