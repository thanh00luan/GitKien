using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObject.Model;

namespace Repository.IRepository
{
    public interface OptionIRepository
    {
        Task<List<Option>> GetAllOptionsAsync();
        Task<Option> GetOptionByIdAsync(int id);
        Task<List<Option>> AddOptionAsync(List<Option> option);
        Task<Option> UpdateOptionAsync(Option option);
        Task<Option> DeleteOptionAsync(int id);
        bool OptionExists(int id);
    }
}
