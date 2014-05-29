using System;
using CodeWarrior.DAL.DbContext;
using CodeWarrior.DAL.Repositories;
using CodeWarrior.Model;
using Faker;
using MongoDB.Bson;

namespace DataSeeder
{
    internal class Seeder
    {
        public static void Seed()
        {
            SeedQuestions();
            Console.ReadKey();
        }

        private static void SeedQuestions()
        {
            var question = new Question
            {
                CreatedBy = ObjectId.GenerateNewId().ToString(),
                Title = TextFaker.Sentence(),
                Description = TextFaker.Sentences(5)
            };
            var repository = new QuestionRepository(new ApplicationDbContext());
            repository.Insert(question);
            Console.Write(repository.FindAll().ToJson());
            
        }
    }
}