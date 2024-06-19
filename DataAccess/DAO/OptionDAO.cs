using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Model;

namespace DataAccess.DAO
{
    public class OptionDAO
    {
        private readonly DBContext _context;

        public OptionDAO(DBContext context)
        {
            _context = context;
        }

        public async Task<List<Option>> GetAllOptionsAsync()
        {
            return await _context.Options.ToListAsync();
        }

        public async Task<Option> GetOptionByIdAsync(int id)
        {
            return await _context.Options.FindAsync(id);
        }

        public async Task<List<Option>> AddOptionAsync(List<Option> option)
        {
            _context.Options.AddRange(option);
            await _context.SaveChangesAsync();
            return option;
        }

        public async Task<Option> UpdateOptionAsync(Option option)
        {
            _context.Entry(option).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return option;
        }

        public async Task<Option> DeleteOptionAsync(int id)
        {
            var option = await _context.Options.FindAsync(id);
            if (option == null)
            {
                return null;
            }

            _context.Options.Remove(option);
            await _context.SaveChangesAsync();
            return option;
        }

        public bool OptionExists(int id)
        {
            return _context.Options.Any(e => e.OptionId == id);
        }
    }
}
