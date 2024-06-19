using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface QuestionIRepository
    {
        Task<List<Question>> GetAllQuestionsAsync();
        Task<List<Question>> GetQuestionByIdAsync(int id);
        Task<List<Question>> CreateQuestionsAndOptionsAsync(List<Question> questions);
        Task<Question> UpdateQuestionAsync(Question question);
        Task<Question> DeleteQuestionAsync(int id);
        bool QuestionExists(int id);
    }
}
