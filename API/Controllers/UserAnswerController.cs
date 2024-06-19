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
    public class UserAnswerController : ControllerBase
    {
        private readonly UserAnswerIRepository _userAnswerRepository;

        public UserAnswerController(UserAnswerIRepository userAnswerRepository)
        {
            _userAnswerRepository = userAnswerRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserAnswer>>> GetAllUserAnswersAsync()
        {
            var userAnswers = await _userAnswerRepository.GetAllUserAnswersAsync();
            return Ok(userAnswers);
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<UserAnswer>> GetUserAnswerByIdAsync(int id)
        //{
        //    var userAnswer = await _userAnswerRepository.GetUserAnswerByIdAsync(id);
        //    if (userAnswer == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(userAnswer);
        //}

        //[HttpPost]
        //public async Task<ActionResult<UserAnswer>> AddUserAnswerAsync(UserAnswer userAnswer)
        //{
        //    var addedUserAnswer = await _userAnswerRepository.AddUserAnswerAsync(userAnswer);
        //    return Ok(addedUserAnswer);
        //}
    }
}
