using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObject.DTO;
using BusinessObject.Model;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserExamController : ControllerBase
    {
        private readonly UserExamIRepository _userExamRepository;

        public UserExamController(UserExamIRepository userExamRepository)
        {
            _userExamRepository = userExamRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserExamDto>>> GetAllUserExamsAsync(int limit, int offset)
        {
            var userExams = await _userExamRepository.GetAllUserExamsAsync(limit, offset);
            return Ok(userExams);
        }
        [HttpGet("{userId}")]
        public async Task<ActionResult<List<ExamUserDTO>>> GetAllUserExamsByIdAsync(int userId)
        {
            var userExams = await _userExamRepository.GetAllUserExamsByIdAsync(userId);
            return Ok(userExams);
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<UserExam>> GetUserExamByIdAsync(int id)
        //{
        //    var userExam = await _userExamRepository.GetUserExamByIdAsync(id);
        //    if (userExam == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(userExam);
        //}

        //[HttpPost]
        //public async Task<ActionResult<UserExam>> AddUserExamAsync(UserExam userExam)
        //{
        //    var addedUserExam = await _userExamRepository.AddUserExamAsync(userExam);
        //    return Ok(addedUserExam);
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateUserExamAsync(int id, UserExam userExam)
        //{
        //    if (id != userExam.UserExamId)
        //    {
        //        return BadRequest();
        //    }

        //    var updatedUserExam = await _userExamRepository.UpdateUserExamAsync(userExam);
        //    if (updatedUserExam == null)
        //    {
        //        return NotFound();
        //    }

        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUserExamAsync(int id)
        //{
        //    var deletedUserExam = await _userExamRepository.DeleteUserExamAsync(id);
        //    if (deletedUserExam == null)
        //    {
        //        return NotFound();
        //    }

        //    return NoContent();
        //}
    }
}
