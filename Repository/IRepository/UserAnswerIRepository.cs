using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObject.Model;

namespace Repository.IRepository
{
    public interface UserAnswerIRepository
    {
        Task<List<UserAnswer>> GetAllUserAnswersAsync();
        Task<UserAnswer> GetUserAnswerByIdAsync(int id);
        Task<UserAnswer> AddUserAnswerAsync(UserAnswer userAnswer);
        Task<UserAnswer> UpdateUserAnswerAsync(UserAnswer userAnswer);
        Task<UserAnswer> DeleteUserAnswerAsync(int id);
        bool UserAnswerExists(int id);
    }
}
