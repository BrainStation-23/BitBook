using System;
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
            SeedQuestions();
            SeedUsers();

            Console.ReadKey();
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