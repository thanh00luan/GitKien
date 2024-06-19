using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObject.Model;
using DataAccess.DAO;
using Repository.IRepository;

namespace DataAccess.Repository
{

    public class QuestionRepository : QuestionIRepository
    {
        private readonly QuestionDAO _questionDAO;

        public QuestionRepository(QuestionDAO questionDAO)
        {
            _questionDAO = questionDAO;
        }

        public async Task<List<Question>> GetAllQuestionsAsync()
        {
            return await _questionDAO.GetAllQuestionsAsync();
        }

        public async Task<List<Question>> GetQuestionByIdAsync(int id)
        {
            return await _questionDAO.GetQuestionByIdAsync(id);
        }

        public async Task<List<Question>> CreateQuestionsAndOptionsAsync(List<Question> questions)
        {
            return await _questionDAO.CreateQuestionsAndOptionsAsync(questions);
        }

        public async Task<Question> UpdateQuestionAsync(Question question)
        {
            return await _questionDAO.UpdateQuestionAsync(question);
        }

        public async Task<Question> DeleteQuestionAsync(int id)
        {
            return await _questionDAO.DeleteQuestionAsync(id);
        }

        public bool QuestionExists(int id)
        {
            return _questionDAO.QuestionExists(id);
        }
    }
}
