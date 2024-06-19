using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObject.Model;
using DataAccess.DAO;
using DataAccess;
using System;
using DataAccess.Repository;
using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CsvHelper;
using CsvHelper.Configuration;
using System.IO;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly ExamIRepository _examRepository;
        private readonly ILogger<ExamController> _logger;
        private readonly DBContext _context;


        public ExamController(ExamIRepository examRepository, ILogger<ExamController> logger, DBContext context)
        {
            _examRepository = examRepository;
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<object>> GetAllExamsAsync(int id)
        {
            var exams = await _examRepository.GetAllExamsAsync(id);
            return Ok(exams);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Exam>>> GetExamByIdAsync()
        {
            var exam = await _examRepository.GetExamByIdAsync();
            if (exam == null)
            {
                return NotFound();
            }
            return Ok(exam);
        }
        [HttpPost]
        [Route("submit-exam")]
        public async Task<IActionResult> SubmitExam([FromBody] SubmitExamModel model)
        {
            try
            {
                await _examRepository.SubmitExam(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ExamResult/{userExamId}")]
        public IActionResult GetExamDetail(int userExamId)
        {
            var userExam = _context.UserExams.FirstOrDefault(u => u.UserExamId == userExamId);

            if (userExam == null)
            {
                return NotFound();
            }

            var examDetail = new
            {
                UserExam = userExam,
                Questions = GetUserExamQuestions(userExamId)
            };

            return Ok(examDetail);
        }

        private IEnumerable<object> GetUserExamQuestions(int userExamId)
        {
            var userAnswers = _context.UserAnswers.Where(u => u.UserExamId == userExamId).ToList();
            var questions = new List<object>();

            foreach (var userAnswer in userAnswers)
            {
                var question = _context.Questions.FirstOrDefault(q => q.QuestionId == userAnswer.QuestionId);
                var options = _context.Options.Where(o => o.QuestionId == userAnswer.QuestionId).ToList();
                var chosenOption = _context.Options.FirstOrDefault(o => o.OptionId == userAnswer.ChosenOptionId);

                var questionDetail = new
                {
                    Question = question,
                    Options = options,
                    ChosenOption = chosenOption
                };

                questions.Add(questionDetail);
            }

            return questions;
        }
        [HttpPost]
        public async Task<ActionResult<Exam>> AddExamAsync(Exam exam)
        {
            var addedExam = await _examRepository.AddExamAsync(exam);
            return Ok(addedExam);
        }
        [HttpPost("import")]
        public async Task<IActionResult> ImportCsv(IFormFile csvFile)
        {
            if (csvFile == null || csvFile.Length == 0)
                return BadRequest("Không có file CSV được tải lên.");

            using var reader = new StreamReader(csvFile.OpenReadStream());
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false
            };
            using var csv = new CsvReader(reader, csvConfig);

            var exams = new List<Exam>();
            var questions = new List<Question>();
            var options = new List<Option>();

            Exam currentExam = null;
            Question currentQuestion = null;
            while (csv.Read())
            {
                var recordType = csv.GetField<string>(0);

                switch (recordType)
                {
                    case "Title":
                        var title = csv.GetField<string>(1);
                        var description = csv.GetField<string>(2);
                        var startTime = csv.GetField<DateTime>(3);
                        var endTime = csv.GetField<DateTime>(4);
                        var status = csv.GetField<string>(5);

                        currentExam = new Exam
                        {
                            Title = title,
                            Description = description,
                            StartTime = startTime,
                            EndTime = endTime,
                            Status = status
                        };
                        exams.Add(currentExam);
                        break;

                    case "Question":
                        var questionContent = csv.GetField<string>(1);
                        var questionType = csv.GetField<string>(2);
                        var questionStatus = csv.GetField<string>(3);

                        currentQuestion = new Question
                        {
                            Content = questionContent,
                            Type = questionType,
                            Status = questionStatus,
                            ExamId = currentExam.ExamId // Get the ExamId of the current exam
                        };
                        questions.Add(currentQuestion);
                        break;

                    case "Option":
                        var optionContent = csv.GetField<string>(1);
                        var isCorrect = csv.GetField<bool>(2);
                        var optionStatus = csv.GetField<string>(3);

                        var option = new Option
                        {
                            Content = optionContent,
                            IsCorrect = isCorrect,
                            Status = optionStatus,
                            QuestionId = currentQuestion.QuestionId // Get the QuestionId of the current question
                        };
                        options.Add(option);
                        break;
                }
            }

            _context.Exams.AddRange(exams);
            await _context.SaveChangesAsync(); // Lưu Exams trước để có ExamId

            foreach (var question in questions)
            {
                question.ExamId = currentExam.ExamId; 
            }
            _context.Questions.AddRange(questions);
            await _context.SaveChangesAsync(); 
            int count = 0;
            foreach (var question in questions)
            {
                foreach (var option in options)
                {
                    if(option.QuestionId == 0)
                    {
                        option.QuestionId = question.QuestionId;
                        count++;
                        if (count == 4)
                        {
                            count = 0;
                            break;
                        }
                    }
                }
            }
            _context.Options.AddRange(options);
            await _context.SaveChangesAsync(); 

            return Ok();
        }
        [HttpGet("export")]
        public IActionResult ExportCsv(int id)
        {
            var csvRecords = new List<string>();

            var exam = _context.Exams.FirstOrDefault(e => e.ExamId == id);
            if (exam == null)
            {
                return BadRequest("Không tìm thấy bài thi.");
            }

            csvRecords.Add($"\"Title\",\"{exam.Title}\",\"{exam.Description}\",\"{exam.StartTime}\",\"{exam.EndTime}\",\"{exam.Status}\"");

            var questions = _context.Questions.Where(q => q.ExamId == id).ToList();
            foreach (var question in questions)
            {
                csvRecords.Add($"\"Question\",\"{question.Content}\",\"{question.Type}\",\"{question.Status}\"");

                var options = _context.Options.Where(o => o.QuestionId == question.QuestionId).ToList();

                foreach (var option in options)
                {
                    csvRecords.Add($"\"Option\",\"{option.Content}\",\"{option.IsCorrect}\",\"{option.Status}\"");
                }
            }

            var csvContent = string.Join("\n", csvRecords);

            var fileName = "export.csv";
            return File(new System.Text.UTF8Encoding().GetBytes(csvContent), "text/csv", fileName);
        }

    }
}
