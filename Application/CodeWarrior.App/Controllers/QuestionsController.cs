using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CodeWarrior.App.ViewModels;
using CodeWarrior.App.ViewModels.Questions;
using CodeWarrior.DAL.DbContext;
using CodeWarrior.DAL.Interfaces;
using CodeWarrior.Model;
using MongoDB.Driver;

namespace CodeWarrior.App.Controllers
{
    [Authorize]
    [RoutePrefix("api/Questions")]
    public class QuestionsController : BaseApiController
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionsController(IApplicationDbContext applicationDbContext, IQuestionRepository questionRepository)
            : base(applicationDbContext)
        {
            _questionRepository = questionRepository;
        }

        // GET api/questions
        public IEnumerable<Question> Get()
        {
            return _questionRepository.FindAll();
        }

        // GET api/questions/5
        public Question Get(string id)
        {
            return _questionRepository.FindById(id);
        }

        // POST api/questions
        public IHttpActionResult Post(QuestionBindingModel question)
        {
            if (ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ques = AutoMapper.Mapper.Map<QuestionBindingModel, Question>(question);
            _questionRepository.Insert(ques);

            return Ok(ques);
        }

        // PUT api/questions/5
        public void Put(Question question)
        {

        }

        // DELETE api/questions/5
        public void Delete(int id)
        {

        }
    }
}
