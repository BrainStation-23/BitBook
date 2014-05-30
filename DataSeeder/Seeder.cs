using System;
using CodeWarrior.DAL.DbContext;
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
            Console.ReadKey();
        }

        private static void SeedQuestions()
        {
            var question = new Question
            {
                CreatedBy = ObjectId.GenerateNewId().ToString(),
                Title = "dfgd df dfg fgdflgk dlfkg  dlfgjk",
                Description = "dfg dlfgk dfklgdfgjk\n dflkgj dlfkgdlfkgjdflkgjdfklg\n ldkfgldkfg\n dnflgkdlfkgj d"
            };

            var repository = new QuestionRepository(new ApplicationDbContext());

            repository.Insert(question);

            Console.Write(repository.Where().ToJson());
            
        }
    }
}