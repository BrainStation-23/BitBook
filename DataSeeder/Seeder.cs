using System;
using System.Collections.Generic;
using System.Linq;
using CodeWarrior.DAL.DbContext;
using CodeWarrior.DAL.Interfaces;
using CodeWarrior.DAL.Repositories;
using CodeWarrior.Model;
using MongoDB.Bson;

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

            foreach (var user in GetAllUser())
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

        }

        private static void SeedUsers()
        {
            
            IUserRepository repository = new UserRepository(new ApplicationDbContext());
            //repository.Insert();
        }

        private static void SeedQuestions()
        {
            var question = new Question
            {
                CreatedBy = ObjectId.GenerateNewId().ToString(),
                Title = Faker.TextFaker.Sentence(),
                Description = Faker.TextFaker.Sentence()
            };

            var repository = new QuestionRepository(new ApplicationDbContext());

            repository.Insert(question);

            Console.Write(repository.Where().ToJson());
            
        }
    }
}