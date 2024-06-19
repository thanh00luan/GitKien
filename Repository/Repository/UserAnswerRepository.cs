using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObject.Model;
using DataAccess.DAO;
using Repository.IRepository;

namespace DataAccess.Repository
{
    public class UserAnswerRepository : UserAnswerIRepository
    {
        private readonly UserAnswerDAO _userAnswerDAO;

        public UserAnswerRepository(UserAnswerDAO userAnswerDAO)
        {
            _userAnswerDAO = userAnswerDAO;
        }

        public async Task<List<UserAnswer>> GetAllUserAnswersAsync()
        {
            return await _userAnswerDAO.GetAllUserAnswersAsync();
        }

        public async Task<UserAnswer> GetUserAnswerByIdAsync(int id)
        {
            return await _userAnswerDAO.GetUserAnswerByIdAsync(id);
        }

        public async Task<UserAnswer> AddUserAnswerAsync(UserAnswer userAnswer)
        {
            return await _userAnswerDAO.AddUserAnswerAsync(userAnswer);
        }

        public async Task<UserAnswer> UpdateUserAnswerAsync(UserAnswer userAnswer)
        {
            return await _userAnswerDAO.UpdateUserAnswerAsync(userAnswer);
        }

        public async Task<UserAnswer> DeleteUserAnswerAsync(int id)
        {
            return await _userAnswerDAO.DeleteUserAnswerAsync(id);
        }

        public bool UserAnswerExists(int id)
        {
            return _userAnswerDAO.UserAnswerExists(id);
        }
    }
}
