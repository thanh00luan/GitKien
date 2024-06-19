using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Model;
using System.Linq;

namespace DataAccess.DAO
{
    public class QuestionDAO
    {
        private readonly DBContext _context;

        public QuestionDAO(DBContext context)
        {
            _context = context;
        }

        public async Task<List<Question>> GetAllQuestionsAsync()
        {
            return await _context.Questions.ToListAsync();
        }

        public async Task<List<Question>> GetQuestionByIdAsync(int id)
        {
            return await _context.Questions.Where(q => q.ExamId == id).ToListAsync();
        }

        public async Task<List<Question>> CreateQuestionsAndOptionsAsync(List<Question> questions)
        {
            foreach (var question in questions)
            {
                _context.Questions.Add(question);
            }

            await _context.SaveChangesAsync();
            return questions;
        }

        public async Task<Question> UpdateQuestionAsync(Question question)
        {
            _context.Entry(question).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return question;
        }

        public async Task<Question> DeleteQuestionAsync(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return null;
            }

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return question;
        }

        public bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.QuestionId == id);
        }
    }
}
