using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObject.Model;
using DataAccess.DAO;

namespace DataAccess.IRepository
{
    public interface ExamIRepository
    {
        Task<object> GetAllExamsAsync();
        Task<Exam> GetExamByIdAsync(int id);
        Task<Exam> AddExamAsync(Exam exam);
        Task<Exam> UpdateExamAsync(Exam exam);
        Task<Exam> DeleteExamAsync(int id);
        bool ExamExists(int id);
    }
}
