using BusinessObject.Model;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly QuestionIRepository _questionRepository;

        public QuestionController(QuestionIRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public class QuestionAndOptionDTO
        {
            public List<Question> Questions { get; set; }
        }

        [HttpPost]
        public async Task<ActionResult<List<Question>>> AddQuestionAsync([FromBody] QuestionAndOptionDTO Questions)
        {
            var addedQuestion = await _questionRepository.CreateQuestionsAndOptionsAsync(Questions.Questions);
            return Ok(addedQuestion);
        }
    }
}
