using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObject.Model;
using DataAccess.DAO;
using Repository.IRepository;

namespace DataAccess.Repository
{
   
    public class OptionRepository : OptionIRepository
    {
        private readonly OptionDAO _optionDAO;

        public OptionRepository(OptionDAO optionDAO)
        {
            _optionDAO = optionDAO;
        }

        public async Task<List<Option>> GetAllOptionsAsync()
        {
            return await _optionDAO.GetAllOptionsAsync();
        }

        public async Task<Option> GetOptionByIdAsync(int id)
        {
            return await _optionDAO.GetOptionByIdAsync(id);
        }

        public async Task<List<Option>> AddOptionAsync(List<Option> option)
        {
            return await _optionDAO.AddOptionAsync(option);
        }

        public async Task<Option> UpdateOptionAsync(Option option)
        {
            return await _optionDAO.UpdateOptionAsync(option);
        }

        public async Task<Option> DeleteOptionAsync(int id)
        {
            return await _optionDAO.DeleteOptionAsync(id);
        }

        public bool OptionExists(int id)
        {
            return _optionDAO.OptionExists(id);
        }
    }
}
