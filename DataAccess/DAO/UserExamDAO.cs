using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Model;
using System.Linq;
using BusinessObject.DTO;

namespace DataAccess.DAO
{
    public class ExamUserDAO
    {
        private readonly DBContext _context;

        public ExamUserDAO(DBContext context)
        {
            _context = context;
        }

        public async Task<List<UserExam>> GetAllUserExamsAsync()
        {
            return await _context.UserExams.ToListAsync();
        }
        public async Task<List<ExamUserDTO>> GetAllUserExamsByIdAsync(int userId)
        {
            var userExams = await _context.UserExams
                .Where(d => d.UserId == userId)
                .ToListAsync();

            var examUserDTOs = new List<ExamUserDTO>();

            foreach (var userExam in userExams)
            {
                var exam = await _context.Exams
                    .FirstOrDefaultAsync(e => e.ExamId == userExam.ExamId);

                if (exam != null)
                {
                    var examUserDTO = new ExamUserDTO
                    {
                        UserId = userExam.UserId,
                        ExamId = userExam.ExamId,
                        Score = userExam.Score,
                        Status = userExam.Status,
                        Title = exam.Title,
                        StartTime = userExam.StartTime,
                        EndTime = userExam.EndTime,
                        UserExamId = userExam.UserExamId,
                    };

                    examUserDTOs.Add(examUserDTO);
                }
            }

            return examUserDTOs;
        }

        public async Task<UserExam> GetUserExamByIdAsync(int id)
        {
            return await _context.UserExams.FindAsync(id);
        }

        public async Task<UserExam> AddUserExamAsync(UserExam userExam)
        {
            _context.UserExams.Add(userExam);
            await _context.SaveChangesAsync();
            return userExam;
        }

        public async Task<UserExam> UpdateUserExamAsync(UserExam userExam)
        {
            _context.Entry(userExam).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return userExam;
        }

        public async Task<UserExam> DeleteUserExamAsync(int id)
        {
            var userExam = await _context.UserExams.FindAsync(id);
            if (userExam == null)
            {
                return null;
            }

            _context.UserExams.Remove(userExam);
            await _context.SaveChangesAsync();
            return userExam;
        }

        public bool UserExamExists(int id)
        {
            return _context.UserExams.Any(e => e.UserExamId == id);
        }
    }
}
