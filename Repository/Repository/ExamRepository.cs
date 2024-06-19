using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Model;
using DataAccess.DAO;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public interface ExamIRepository
    {
        Task<object> GetAllExamsAsync(int id);
        Task<List<Exam>> GetExamByIdAsync();
        Task<Exam> AddExamAsync(Exam exam);
        Task<Exam> UpdateExamAsync(Exam exam);
        Task<Exam> DeleteExamAsync(int id);
        Task SubmitExam(SubmitExamModel submitExamModel);

        bool ExamExists(int id);
    }

    public class ExamRepository : ExamIRepository
    {
        private readonly DBContext _context;

        public ExamRepository(DBContext context)
        {
            _context = context;
        }
        public async Task SubmitExam(SubmitExamModel model)
        {
            var userExam = new UserExam
            {
                UserId = model.UserId,
                ExamId = model.ExamId,
                StartTime = model.StartTime,
                EndTime = model.EndTime.AddHours(+7),
                Score = model.Score,
                Status = model.Status
            };

            _context.UserExams.Add(userExam);
            await _context.SaveChangesAsync();

            // Lưu các câu trả lời của người dùng cho các câu hỏi (UserAnswer)
            foreach (var answer in model.UserAnswers)
            {
                var userAnswer = new UserAnswer
                {
                    UserExamId = userExam.UserExamId,
                    QuestionId = answer.QuestionId,
                    ChosenOptionId = answer.AnswerId,
                    Status = "Complete"
                };

                _context.UserAnswers.Add(userAnswer);
            }

            await _context.SaveChangesAsync();
        }
        public async Task<object> GetAllExamsAsync(int id)
        {
            var exam = await _context.Exams
                .Where(e => e.ExamId == id)
                .FirstOrDefaultAsync();

            if (exam == null)
            {
                return null;
            }

            // Lấy các câu hỏi liên quan đến bài thi
            var questions = await _context.Questions
                .Where(q => q.ExamId == id)
                .ToListAsync();

            // Lấy các tùy chọn cho từng câu hỏi
            var questionIds = questions.Select(q => q.QuestionId).ToList();
            var options = await _context.Options
                .Where(o => questionIds.Contains(o.QuestionId))
                .ToListAsync();

            // Tổ chức dữ liệu trả về
            var result = new
            {
                ExamId = exam.ExamId,
                Title = exam.Title,
                Description = exam.Description,
                StartTime = exam.StartTime,
                EndTime = exam.EndTime,
                Status = exam.Status,
                Questions = questions.Select(q => new
                {
                    QuestionId = q.QuestionId,
                    Content = q.Content,
                    Type = q.Type,
                    Status = q.Status,
                    Options = options.Where(o => o.QuestionId == q.QuestionId).ToList()
                }).ToList()
            };

            return result;
        }

        public async Task<List<Exam>> GetExamByIdAsync()
        {
            return await _context.Exams.ToListAsync();
        }

        public async Task<Exam> AddExamAsync(Exam exam)
        {
            _context.Exams.Add(exam);
            await _context.SaveChangesAsync();
            return exam;
        }

        public async Task<Exam> UpdateExamAsync(Exam exam)
        {
            _context.Entry(exam).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return exam;
        }

        public async Task<Exam> DeleteExamAsync(int id)
        {
            var exam = await _context.Exams.FindAsync(id);
            if (exam == null)
            {
                return null;
            }

            _context.Exams.Remove(exam);
            await _context.SaveChangesAsync();
            return exam;
        }

        public bool ExamExists(int id)
        {
            return _context.Exams.Any(e => e.ExamId == id);
        }
    }
}
