using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObject.DTO;
using BusinessObject.Model;
using DataAccess.DAO;

namespace Repository.IRepository
{
    public interface UserExamIRepository
    {
        Task<List<UserExamDto>> GetAllUserExamsAsync(int limit, int offset);
        Task<UserExam> GetUserExamByIdAsync(int id);
        Task<UserExam> AddUserExamAsync(UserExam userExam);
        Task<UserExam> UpdateUserExamAsync(UserExam userExam);
        Task<UserExam> DeleteUserExamAsync(int id);
        bool UserExamExists(int id);
        Task<List<ExamUserDTO>> GetAllUserExamsByIdAsync(int userId);
    }
}
