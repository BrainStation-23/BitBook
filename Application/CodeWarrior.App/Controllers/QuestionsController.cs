using CodeWarrior.App.ViewModels.Questions;
using CodeWarrior.DAL.DbContext;
using CodeWarrior.DAL.Interfaces;
using CodeWarrior.Model;
using System.Collections.Generic;
using System.Web.Http;

namespace CodeWarrior.App.Controllers
{
    //[Authorize]
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var questionModel = AutoMapper.Mapper.Map<QuestionBindingModel, Question>(question);
            _questionRepository.Insert(questionModel);

            return Ok(questionModel);
        }

        // PUT api/questions/question
        public IHttpActionResult Put(QuestionBindingModel question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var questionModel = AutoMapper.Mapper.Map<QuestionBindingModel, Question>(question);
            var dbQuestion = _questionRepository.FindById(question.Id);

            dbQuestion.Title = questionModel.Title;
            dbQuestion.Description = questionModel.Description;
            dbQuestion.UpVote = questionModel.UpVote;
            dbQuestion.DownVote = questionModel.DownVote;
            dbQuestion.Tags = questionModel.Tags;

            _questionRepository.Update(dbQuestion);

            return Ok();
        }

        // DELETE api/questions/5
        public void Delete(string id)
        {
            _questionRepository.Remove(id);
        }
    }
}
