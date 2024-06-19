using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Model;
using System.Linq;

namespace DataAccess.DAO
{
    public class UserAnswerDAO
    {
        private readonly DBContext _context;

        public UserAnswerDAO(DBContext context)
        {
            _context = context;
        }

        public async Task<List<UserAnswer>> GetAllUserAnswersAsync()
        {
            return await _context.UserAnswers.ToListAsync();
        }

        public async Task<UserAnswer> GetUserAnswerByIdAsync(int id)
        {
            return await _context.UserAnswers.FindAsync(id);
        }

        public async Task<UserAnswer> AddUserAnswerAsync(UserAnswer userAnswer)
        {
            _context.UserAnswers.Add(userAnswer);
            await _context.SaveChangesAsync();
            return userAnswer;
        }

        public async Task<UserAnswer> UpdateUserAnswerAsync(UserAnswer userAnswer)
        {
            _context.Entry(userAnswer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return userAnswer;
        }

        public async Task<UserAnswer> DeleteUserAnswerAsync(int id)
        {
            var userAnswer = await _context.UserAnswers.FindAsync(id);
            if (userAnswer == null)
            {
                return null;
            }

            _context.UserAnswers.Remove(userAnswer);
            await _context.SaveChangesAsync();
            return userAnswer;
        }

        public bool UserAnswerExists(int id)
        {
            return _context.UserAnswers.Any(e => e.UserAnswerId == id);
        }
    }
}
