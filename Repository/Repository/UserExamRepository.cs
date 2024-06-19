using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObject.DTO;
using BusinessObject.Model;
using DataAccess.DAO;
using Repository.IRepository;

namespace DataAccess.Repository
{
    public class UserExamRepository : UserExamIRepository
    {
        private readonly ExamUserDAO _userExamDAO;

        public UserExamRepository(ExamUserDAO userExamDAO)
        {
            _userExamDAO = userExamDAO;
        }

        public async Task<List<UserExam>> GetAllUserExamsAsync()
        {
            return await _userExamDAO.GetAllUserExamsAsync();
        }
        public Task<List<ExamUserDTO>> GetAllUserExamsByIdAsync(int userId)
        {
            return _userExamDAO.GetAllUserExamsByIdAsync(userId);
        }
        public async Task<UserExam> GetUserExamByIdAsync(int id)
        {
            return await _userExamDAO.GetUserExamByIdAsync(id);
        }

        public async Task<UserExam> AddUserExamAsync(UserExam userExam)
        {
            return await _userExamDAO.AddUserExamAsync(userExam);
        }

        public async Task<UserExam> UpdateUserExamAsync(UserExam userExam)
        {
            return await _userExamDAO.UpdateUserExamAsync(userExam);
        }

        public async Task<UserExam> DeleteUserExamAsync(int id)
        {
            return await _userExamDAO.DeleteUserExamAsync(id);
        }

        public bool UserExamExists(int id)
        {
            return _userExamDAO.UserExamExists(id);
        }
    }
}
