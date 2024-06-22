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

        public async Task<List<UserExamDto>> GetAllUserExamsAsync(int limit, int offset)
        {
            var userExams = await _context.UserExams
                                          .OrderByDescending(ue => ue.Score) // Order by Score descending
                                          .Skip(offset)                     // Skip the specified number of elements
                                          .Take(limit)                      // Take the specified number of elements
                                          .ToListAsync();                   // Convert to a list asynchronously

            var userExamDtos = new List<UserExamDto>();

            foreach (var ue in userExams)
            {
                var user = await _context.Users
                                         .Where(u => u.UserId == ue.UserId)
                                         .FirstOrDefaultAsync();

                var exam = await _context.Exams
                                         .Where(e => e.ExamId == ue.ExamId)
                                         .FirstOrDefaultAsync();

                userExamDtos.Add(new UserExamDto
                {
                    UserExamId = ue.UserExamId,
                    FullName = user?.FullName, // Use null-conditional operator to handle possible null values
                    Title = exam?.Title,       // Use null-conditional operator to handle possible null values
                    StartTime = ue.StartTime,
                    EndTime = ue.EndTime,
                    Score = ue.Score,
                    Status = ue.Status
                });
            }

            return userExamDtos;
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
