using CodeWarrior.DAL.DbContext;
using CodeWarrior.DAL.Interfaces;
using CodeWarrior.DAL.Repositories;
using CodeWarrior.Model;
using Microsoft.AspNet.Identity;
using MongoDB.AspNet.Identity;
using System;
using System.Collections.Generic;

namespace DataSeeder
{
    internal class Seeder
    {
        public static void Seed()
        {
            //SeedQuestions();
            //SeedUsers();
            SeedPosts();

            Console.ReadKey();
        }

        private static MongoDB.Driver.MongoCursor<ApplicationUser> GetAllUser()
        {
           IUserRepository userRepository = new UserRepository(new ApplicationDbContext());
           return userRepository.FindAll();
        }

        private static void SeedPosts()
        {
            IPostRepository repository = new PostRepository(new ApplicationDbContext());
            var users = GetAllUser();

            
            foreach (var user in users)
            {
                for (var i = 0; i < 100; i++)
                {
                    repository.Insert(
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
                            LikedBy = new List<string> {user.Id}
                        }
                        );
                }
            }

            var us = new PostRepository(new ApplicationDbContext()).FindAll().Count();
            Console.Write(us);

        }

        private async static void SeedUsers()
        {
            var userManage = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext().Database));

            for (var i = 0; i < 100; i++)
            {
                var user = new ApplicationUser
                {
                    FirstName = Faker.NameFaker.FirstName(),
                    LastName = Faker.NameFaker.LastName(),
                    UserName = i + Faker.InternetFaker.Email()
                };
                await userManage.CreateAsync(user, "123456");
            }

            var us = new UserRepository(new ApplicationDbContext()).FindAll().Count();
            Console.Write(us);
        }
    }
}